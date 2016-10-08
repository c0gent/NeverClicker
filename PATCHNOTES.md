Version 0.6.0 (UNRELEASED)
===========================

* The logging system has been completely redesigned.
  * Now uses an [NLog](http://nlog-project.org/) backend.
  * Performance is greatly increased.
  * Log files are no longer in XML format (though the final format hasn't been 
    decided on).
  * Logs now rotate every day and are automatically deleted after one week.
  * A 'Trace' level has been added for very minute details.
* The settings system has been completely redesigned.
  * Settings are now in XML instead of INI format allowing more complex, nested
    configuration settings with the tradeoff that settings files are a bit 
	harder to read. They now have the '.xml.txt' extension.
  * A new 'AccountStates.xml.txt' settings file has been added, separating
    account specific settings from states (the current status of things).
* Various fixes for inv. management, key claiming, and prof's.