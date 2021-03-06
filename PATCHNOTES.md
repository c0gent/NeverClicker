Version 0.6.3 (2016-Oct-15)
===========================
* Attempts to copy the default settings folder upon new installation 
  have been discontinued and no longer cause an exception.

Version 0.6.2 (2016-Oct-15)
===========================
* Fixed an issue causing profession tasks to get stuck in certain situations.

Version 0.6.1 (2016-Oct-15)
===========================
* Fixed extraneous Enchanted Key error message.
* Corrected a slight miscalculation with queue timing when using a footman,
  Guard, or Mercenary in the primary profession asset slot.
* Fixed queue task advancement algorithm issue which was also causing problems 
  with task timings.


Version 0.6.0 (2016-Oct-11)
===========================

* The logging system has been completely redesigned.
  * Now uses an [NLog](http://nlog-project.org/) backend.
  * Performance is greatly increased.
  * Log files are no longer in XML format (though the final format hasn't been 
    decided on and could go back to XML).
  * Logs now rotate every day and are automatically deleted after one week.
  * A 'Trace' level has been added for very minute details.
* The settings system has been completely redesigned.
  * Settings are now in XML instead of INI format allowing more complex, nested
    configuration settings with the tradeoff that settings files are a bit 
	harder to read. They now have the '.xml.txt' extension.
  * A new 'AccountStates.xml.txt' settings file has been added, separating
    account specific settings from states (the current status of things).
* Vault of Piety purchase preference can now be specified on a per-character 
  basis within 'AccountSettings.xml.txt'.
* Custom character names can also be specified in 'AccountSettings.xml.txt' 
  and should display everywhere (instead of 'Character #5', etc.).
* VIP reward bags are now opened automatically.
* Profession task timers now account for the presence and quality of profession 
  assets.
* Lots of fixes for inv. management, key claiming, and prof's.