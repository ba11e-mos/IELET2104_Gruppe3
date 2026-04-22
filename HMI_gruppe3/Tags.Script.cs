//--------------------------------------------------------------
// Press F1 to get help about using script.
// To access an object that is not located in the current class, start the call with Globals.
// When using events and timers be cautious not to generate memoryleaks,
// please see the help for more information.
//---------------------------------------------------------------

namespace Neo.ApplicationFramework.Generated
{
	using System.Windows.Forms;
	using System;
	using System.Drawing;
	using Neo.ApplicationFramework.Tools;
	using Neo.ApplicationFramework.Common.Graphics.Logic;
	using Neo.ApplicationFramework.Controls;
	using Neo.ApplicationFramework.Interfaces;
    
    
	public partial class Tags
	{
		bool oppdatererFraModus = false;
		
		void Styring_frekvens_eller_ventil_ValueChange(System.Object sender, Neo.ApplicationFramework.Interfaces.Events.ValueChangedEventArgs e)
		{
			if (Styring_frekvens_eller_ventil.Value == 1)
				Globals.Tags.Organ.Value = 1;	
			else if (Styring_frekvens_eller_ventil.Value == 2)
				Globals.Tags.Organ.Value = 0;
		}			



		void OppdaterRegulatorType()
		{
			bool harKp = Globals.Tags.Kp.Value > 0.0;
			bool harKi = Globals.Tags.Ki.Value > 0.0;
			bool harKd = Globals.Tags.Kd.Value > 0.0;

			if (harKp && !harKi && !harKd)
				Globals.Tags.Regulatortype.Value = "P";
			else if (harKp && harKi && !harKd)
				Globals.Tags.Regulatortype.Value = "PI";
			else if (harKp && !harKi && harKd)
				Globals.Tags.Regulatortype.Value = "PD";
			else if (harKp && harKi && harKd)
				Globals.Tags.Regulatortype.Value = "PID";
			else
				Globals.Tags.Regulatortype.Value = "Ingen";
		}	
		
		void Kd_ValueChange(System.Object sender, Neo.ApplicationFramework.Interfaces.Events.ValueChangedEventArgs e)
		{
			OppdaterRegulatorType();
		}			
		void Ki_ValueChange(System.Object sender, Neo.ApplicationFramework.Interfaces.Events.ValueChangedEventArgs e)
		{
			OppdaterRegulatorType();
		}			
		void Kp_ValueChange(System.Object sender, Neo.ApplicationFramework.Interfaces.Events.ValueChangedEventArgs e)
		{
			OppdaterRegulatorType();
		}
	
	}
}
