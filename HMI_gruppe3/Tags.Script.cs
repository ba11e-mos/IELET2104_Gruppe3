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
		
		private bool _oppstartFerdig = false;


		void OppdaterRegulatorType()
		{
			if (!_oppstartFerdig) return;
			
			bool harKp = Globals.Tags.Kp.Value > 0.0;
			bool harKi = Globals.Tags.Ki.Value > 0.0;
			bool harKd = Globals.Tags.Kd.Value > 0.0;

			if (Globals.Tags.Regulator.Value != 0){
				Globals.Tags.RegulatorHist.Value = Globals.Tags.Regulator.Value;
			}
			else {
				harKp = false;
				harKi = false;
				harKd = false;
			}

			if (harKp && !harKi && !harKd)
				{Globals.Tags.Regulatortype.Value = "P";
				Globals.Tags.RegulatorTekst.Value = "P";
				Globals.Tags.PID_farger.Value = 1;
				}
			else if (harKp && harKi && !harKd)
				{Globals.Tags.Regulatortype.Value = "PI";
					Globals.Tags.PID_farger.Value = 4;
				}
			else if (harKp && !harKi && harKd)
				{Globals.Tags.Regulatortype.Value = "PD";
					Globals.Tags.PID_farger.Value = 2;
				}
			else if (harKp && harKi && harKd)
				{Globals.Tags.Regulatortype.Value = "PID";
					Globals.Tags.PID_farger.Value = 3;
				}
			else
				{Globals.Tags.Regulatortype.Value = "Ingen";
					Globals.Tags.PID_farger.Value = 0;
				}
			if (Globals.Tags.Regulator.Value > 4){
				Globals.Tags.Auto_man_farger.Value = 3;
			} else {
				Globals.Tags.Auto_man_farger.Value = 2;
			}
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
		
		void Start_ValueOn(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Start_Stopp_farger.Value = 1;	
			Globals.Tags.StatusTxt.Value = 1;
			Globals.Tags.Start.Value = 1;
			Globals.Tags.Stopp.Value = 0;
			Globals.Tags.V1_bool.Value = 1;
			Globals.Tags.V2_bool.Value = 1;
			Globals.Tags.V3_bool.Value = 0;
			Globals.Tags.Alarm_tekst.Value = "Normal";
			_oppstartFerdig = true;
			
		}
		
		void Stopp_ValueOn(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Start_Stopp_farger.Value = 0;	
			Globals.Tags.StatusTxt.Value = 0;
			Globals.Tags.Stopp.Value = 1;
			Globals.Tags.Start.Value = 0;
			Globals.Tags.V1_bool.Value = 0;
			Globals.Tags.V2_bool.Value = 0;
			Globals.Tags.V3_bool.Value = 0;
		}
		
		/*void Modus_ValueOn(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Auto_man_farger.Value = 2;
		}
		
		void Modus_ValueOff(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Auto_man_farger.Value = 1;
		}*/
		
		void Bruk_foroverkobling_ValueOn(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Foroverkobling_farger.Value = 2;
		}
		
		void Bruk_foroverkobling_ValueOff(System.Object sender, System.EventArgs e)
		{
			Globals.Tags.Foroverkobling_farger.Value = 1;
		}
		
		void Regulator_ValueChange(System.Object sender, Neo.ApplicationFramework.Interfaces.Events.ValueChangedEventArgs e)
		{
			OppdaterRegulatorType();
		}
		
		void Reset_alarm_ValueChange(
			System.Object sender,
			Neo.ApplicationFramework.Interfaces.Events.ValueChangedEventArgs e)
		{
			if (Globals.Tags.Reset_alarm.Value == true)
			{
				Globals.Tags.AlarmAktiv.Value = false;
				OppdaterKvitterTekst();
			
			}
		}
		
		
		
		void Modus_ValueChange(System.Object sender, Neo.ApplicationFramework.Interfaces.Events.ValueChangedEventArgs e)
		{
			if (Globals.Tags.Modus.Value == false)
			{
				// Auto er aktiv
				if (Globals.Tags.Regulator.Value > 4){
					Globals.Tags.Auto_man_farger.Value = 3;
				} else {
					Globals.Tags.Auto_man_farger.Value = 2;
				}
				Globals.Tags.sModus.Value = "Auto";
				Globals.Tags.ModusText.Value = "Auto";
				Globals.Tags.Regulator.Value = Globals.Tags.RegulatorHist.Value;
			}
			else if (Globals.Tags.Modus.Value == true)
			{
				// Manuell er aktiv
				Globals.Tags.Auto_man_farger.Value = 1;
				Globals.Tags.sModus.Value = "Manuell";
				Globals.Tags.ModusText.Value = "Manuell";
				Globals.Tags.PID_farger.Value = 0;
				Globals.Tags.Regulator.Value = 0;
			}	
		}
		
		
		void OppdaterKvitterTekst()
		{
			bool kvitterKlar =
				Globals.Tags.AlarmAktiv.Value == true &&
				Globals.Tags.Alarm_høy.Value == false &&
				Globals.Tags.Alarm_kritisk_høy.Value == false &&
				Globals.Tags.Alarm_lav.Value == false &&
				Globals.Tags.Alarm_kritisk_lav.Value == false;

			Globals.Tags.Kvitter_synlig.Value = kvitterKlar;
		}
		
		void Alarm_høy_ValueChange(System.Object sender, Neo.ApplicationFramework.Interfaces.Events.ValueChangedEventArgs e)
		{
			OppdaterKvitterTekst();
			OppdaterAlarmTekst();
		}
		
		void Alarm_kritisk_høy_ValueChange(System.Object sender, Neo.ApplicationFramework.Interfaces.Events.ValueChangedEventArgs e)
		{
			OppdaterKvitterTekst();
			OppdaterAlarmTekst();
		}
		
		void Alarm_kritisk_lav_ValueChange(System.Object sender, Neo.ApplicationFramework.Interfaces.Events.ValueChangedEventArgs e)
		{
			OppdaterKvitterTekst();
			OppdaterAlarmTekst();
		}
		
		void Alarm_lav_ValueChange(System.Object sender, Neo.ApplicationFramework.Interfaces.Events.ValueChangedEventArgs e)
		{
			OppdaterKvitterTekst();
			OppdaterAlarmTekst();
		}
		
		void AlarmAktiv_ValueChange(System.Object sender, Neo.ApplicationFramework.Interfaces.Events.ValueChangedEventArgs e)
		{
			OppdaterKvitterTekst();
			OppdaterAlarmTekst();
		}
		
		void OppdaterAlarmTekst()
		{
			if (Globals.Tags.Alarm_kritisk_høy.Value == true)
			{
				Globals.Tags.Alarm_tekst.Value = "Kritisk høy";
			}
			else if (Globals.Tags.Alarm_høy.Value == true)
			{
				Globals.Tags.Alarm_tekst.Value = "Høy alarm";
			}
			else if (Globals.Tags.Alarm_kritisk_lav.Value == true)
			{
				Globals.Tags.Alarm_tekst.Value = "Kritisk lav";
			}
			else if (Globals.Tags.Alarm_lav.Value == true)
			{
				Globals.Tags.Alarm_tekst.Value = "Lav alarm";
			}
			else
			{
				Globals.Tags.Alarm_tekst.Value = "Normal";
			}
		}
		
		
		
		
		/*void Styring_frekvens_eller_ventil_ValueChange(
			System.Object sender,
			Neo.ApplicationFramework.Interfaces.Events.ValueChangedEventArgs e)
		{
			if (Globals.Tags.Styring_frekvens_eller_ventil.Value == 1)
			{
				Globals.Tags.Test_verdi.Value = 111;
				Globals.Tags.Organ.Value = 1;
			}
			else if (Globals.Tags.Styring_frekvens_eller_ventil.Value == 0)
			{
				Globals.Tags.Test_verdi.Value = 222;
				Globals.Tags.Organ.Value = 0;
			}
		}*/

	
	}
}
