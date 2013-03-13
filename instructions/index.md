---
title: Configuring WSUS Smart Approve
layout: default
---

##Configuring WSUS Smart Approve

WSUS Smart Approve is a console application that is intended to be run on a regular basis, i.e. as a scheduled task.  The more frequently it is run, the less time there will be between an update being reported as needed, and it being approved for download.

In order to carry out these tasks you need to have administrator access.  The login which you choose to run WSUS Smart Approve under must have administrator access.  You can add an account to your domain specifically for this tool, for now I will leave a search engine as your source for instructions on how to do this.

WSUS Smart Approve is a console application that is intended to be run on a regular basis, i.e. as a scheduled task.  The more frequently it is run, the less time there will be between an update being reported as needed, and it being approved for download.

In order to carry out these tasks you need to have administrator access.  The login which you choose to run WSUS Smart Approve under must have administrator access.  You can add an account to your domain specifically for this tool, for now I will leave a search engine as your source for instructions on how to do this.

###Configuring WSUS
WSUS needs to be set to detect all the classifications and products you wish to be approved.  A pre-exisiting WSUS setup will most likely already be set to do this, you just need to stop it automatically approving everything.

* Open the WSUS MMC Console
* Navigate to Update Services -> %SERVER NAME% -> Options
* Ensure you have chosen the *Products and Classifications* that you want detected
* Choose the Automatic Approvals option
* Edit the *Default Automatic Approval Rule* and any other rule which may exist
* *Untick* any type of update you *do not want to be automatically downloaded*
* Note: If you just want to detect everything and not approve them automatically you want to delete the default rule (as it must have 1 option chosen at all times), this decision is down to you.

###Configuring Smart Approve

Smart Approve uses the xml based configuration file app.config. For details on setting it up see [configuring XML config](xml/ "View instructions on setting up the XML Configuration File").

### Working out the command line

The command line depends on how you have [configed the XML config](xml/ "View instructions on setting up the XML Configuration File").  If you wish to use the *no runset* approach you use the command:

	WsusSmartApprove.exe /norunset

If you wish to use the runset approach the command is

	WsusSmartApprove.exe /runset "name"

If the name of the runset contains a space it *must* be surrounded by quotes.

If you wish to see how your configuration will be applied, without actually applying changes you can use the test mode which is as simple as adding /test onto the relevant command like so:

	WsusSmartApprove.exe /norunset /test
	WsusSmartApprove.exe /runset "name" /test

### Setting up a Scheduled Task

#### Creating the schedule

* Navigate to Control Panel -> Administrative Tools -> Task Scheduler (On Windows Server 2003 and earlier the path is Control Panel -> Scheduled Tasks).
* Choose *Add Scheduled Task*
* Browse to the application and select it (WsusSmartApprove.exe)
* Add the relevant arguements described above
* Initially choose Daily, as we are going to edit this
* Stick with the default times (for now)
* Choose the login you wish to run this with, it must have administrator access in order to work with WSUS.
* Choose *Open advanced properties for this task when i click Finish*
* Decide how often you want to have the tool run, say every 3 hours from 0600 (or a custom pattern of just after people get to work, lunchtime and just after they leave ;))

##### Using multiple schedules
* Change the start time to 06:00
* Check *Show multiple schedules*
* Click the *New* button
* Set a schedule of Daily and a time of 09:00
* Repeat the addition for 12:00, 15:00, 18:00, 21:00, 00:00 and 03:00

##### Using a single schedule
* Alternatively you can use a single schedule
* Set the initial time of say 06:00
* Click advanced
* Check *Repeat task*
* Choose a repeat interval, for example every 3 hours
* Set a duration of 23:55
