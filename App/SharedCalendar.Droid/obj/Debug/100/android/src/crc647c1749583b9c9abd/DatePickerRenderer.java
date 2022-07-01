package crc647c1749583b9c9abd;


public class DatePickerRenderer
	extends crc647c4c06b10f3352ff.MaterialDatePickerRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("SharedCalendar.Droid.Controls.DatePickerRenderer, SharedCalendar.Android", DatePickerRenderer.class, __md_methods);
	}


	public DatePickerRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == DatePickerRenderer.class)
			mono.android.TypeManager.Activate ("SharedCalendar.Droid.Controls.DatePickerRenderer, SharedCalendar.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public DatePickerRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == DatePickerRenderer.class)
			mono.android.TypeManager.Activate ("SharedCalendar.Droid.Controls.DatePickerRenderer, SharedCalendar.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public DatePickerRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == DatePickerRenderer.class)
			mono.android.TypeManager.Activate ("SharedCalendar.Droid.Controls.DatePickerRenderer, SharedCalendar.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}

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
