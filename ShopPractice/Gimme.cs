using System;
using GimmeSingletons;

namespace ShopPractice
{
    public static class Gimme
    {
        public static Device TheDevice = new Device();
    }
}

namespace GimmeSingletons
{
	public struct Device
	{
        public Guid GUID { get { return new Guid(); } }
	}
}
