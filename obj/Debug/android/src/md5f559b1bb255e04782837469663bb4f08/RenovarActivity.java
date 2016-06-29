package md5f559b1bb255e04782837469663bb4f08;


public class RenovarActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("RotativoApp.RenovarActivity, RotativoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RenovarActivity.class, __md_methods);
	}


	public RenovarActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == RenovarActivity.class)
			mono.android.TypeManager.Activate ("RotativoApp.RenovarActivity, RotativoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
