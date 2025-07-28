using System.Runtime.InteropServices;
using System.Text;

namespace Peco.app
{
    internal static class Adapter
    {
        static readonly Guid GUID_NETCLASS = new Guid("4d36e972-e325-11ce-bfc1-08002be10318");

        const uint DIGCF_PROFILE = 0x00000008;
        const uint DIF_REMOVE = 0x00000005;
        const uint DI_REMOVEDEVICE_GLOBAL = 0x00000001;
        const uint SPDRP_FRIENDLYNAME = 0x0000000C;


        [StructLayout(LayoutKind.Sequential)]
        struct SP_DEVINFO_DATA
        {
            public uint cbSize;
            public Guid ClassGuid;
            public uint DevInst;
            public IntPtr Reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct SP_CLASSINSTALL_HEADER
        {
            public uint cbSize;
            public uint InstallFunction;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct SP_REMOVEDEVICE_PARAMS
        {
            public SP_CLASSINSTALL_HEADER ClassInstallHeader;
            public uint Scope;
            public uint HwProfile;
        }

        [DllImport("setupapi.dll", SetLastError = true)]
        static extern IntPtr SetupDiGetClassDevs(
            ref Guid ClassGuid,
            IntPtr Enumerator,
            IntPtr hwndParent,
            uint Flags);

        [DllImport("setupapi.dll", SetLastError = true)]
        static extern bool SetupDiEnumDeviceInfo(
            IntPtr DeviceInfoSet,
            uint MemberIndex,
            ref SP_DEVINFO_DATA DeviceInfoData);

        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool SetupDiGetDeviceRegistryProperty(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            uint Property,
            out uint PropertyRegDataType,
            byte[] PropertyBuffer,
            uint PropertyBufferSize,
            out uint RequiredSize);

        [DllImport("setupapi.dll", SetLastError = true)]
        static extern bool SetupDiSetClassInstallParams(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            ref SP_REMOVEDEVICE_PARAMS ClassInstallParams,
            uint ClassInstallParamsSize);

        [DllImport("setupapi.dll", SetLastError = true)]
        static extern bool SetupDiCallClassInstaller(
            uint InstallFunction,
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData);

        [DllImport("setupapi.dll", SetLastError = true)]
        static extern bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);

        public static bool Remove(string adapterName)
        {
            Guid netClassGuid = GUID_NETCLASS;
            IntPtr devInfo = SetupDiGetClassDevs(ref netClassGuid, IntPtr.Zero, IntPtr.Zero, DIGCF_PROFILE);
            if (devInfo == IntPtr.Zero || devInfo.ToInt64() == -1)
                throw new Exception("SetupDiGetClassDevs failed");

            try
            {
                var devInfoData = new SP_DEVINFO_DATA { cbSize = (uint)Marshal.SizeOf<SP_DEVINFO_DATA>() };
                uint index = 0;

                while (SetupDiEnumDeviceInfo(devInfo, index, ref devInfoData))
                {
                    index++;

                    byte[] buffer = new byte[512];
                    if (!SetupDiGetDeviceRegistryProperty(devInfo, ref devInfoData, SPDRP_FRIENDLYNAME,
                        out _, buffer, (uint)buffer.Length, out uint needed))
                        continue;

                    string name = Encoding.Unicode.GetString(buffer, 0, (int)needed - 2);
                    if (!string.Equals(name, adapterName, StringComparison.OrdinalIgnoreCase))
                        continue;

                    var removeParams = new SP_REMOVEDEVICE_PARAMS
                    {
                        ClassInstallHeader = new SP_CLASSINSTALL_HEADER
                        {
                            cbSize = (uint)Marshal.SizeOf<SP_CLASSINSTALL_HEADER>(),
                            InstallFunction = DIF_REMOVE
                        },
                        Scope = DI_REMOVEDEVICE_GLOBAL,
                        HwProfile = 0
                    };

                    if (!SetupDiSetClassInstallParams(devInfo, ref devInfoData, ref removeParams,
                        (uint)Marshal.SizeOf<SP_REMOVEDEVICE_PARAMS>()))
                    {
                        throw new Exception("SetupDiSetClassInstallParams failed, err=" + Marshal.GetLastWin32Error());
                    }

                    if (!SetupDiCallClassInstaller(DIF_REMOVE, devInfo, ref devInfoData))
                    {
                        throw new Exception("SetupDiCallClassInstaller(DIF_REMOVE) failed, err=" + Marshal.GetLastWin32Error());
                    }

                    return true;
                }
            }
            finally
            {
                SetupDiDestroyDeviceInfoList(devInfo);
            }

            return false;
        }
    }
}