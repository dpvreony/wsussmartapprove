---
title: History
layout: default
---

##History

<ul>
<li><a href="#v1_0_0_6">V1.0.0.6</a></li>
<li><a href="#v1_0_0_5">V1.0.0.5</a></li>
<li><a href="#v1_0_0_4">V1.0.0.4</a></li>
<li><a href="#v1_0_0_3">V1.0.0.3</a></li>
<li><a href="#v1_0_0_2">V1.0.0.2</a></li>
<li><a href="#v1_0_0_1">V1.0.0.1</a></li>
<li><a href="#v1_0_0_0">V1.0.0.0</a></li>
</ul>

<div class="alert alert-info">
For a more detailed history you can <a href="http://wsussmartapprove.codeplex.com/SourceControl/list/changesets" title="view the source code changesets on codeplex">view the source code changesets on codeplex</a>.
</div>

###V1.0.0.6 (coming soon)

* Addition of contribution: Copy approvals between targetgroups.
* removed update scope filter on previously approved items, as it's stopping new target groups getting needed approvals.

###V1.0.0.5 (28 January 2012)

* Declined updates are no longer approved if they have a history of being needed.
* Changed the way the config file is checked for detecting AllClassifications or ProductClassification inside a runset.

###V1.0.0.4 (23 January 2012)

* Fix: Listguids crashing with Unauthorised access exception.
* Fix: version strings and assembly information was wrong.

###V1.0.0.3 (22 January 2012)

* Fix: problem where approved update name's still aren't appearing in console.
* Fix: Creating Install for Update that doesn't support install.

###V1.0.0.2 (22 March 2010)

* Fix: Update name no longer displayed in console.
* Fix: Stale Updates and Superseded logic now correctly filters by Classification (was applying all classifications regardless of config settings).
* New: create installer.
* New: Allow approval rules for specific product.
* Fix: Listguids and SmartApprove now check the user has the necessary permissions on WSUS.

###V1.0.0.1 (12 October 2009)

* feature: Added XML Config schema.
* feature: Ability to specify the server to connect to in the XML config.
* feature: Ability to specify runsets.
* feature: Added Command Line option for specifying which runset to perform.
* feature: Ability to specify rules for target groups and classifications.
* feature: Option to accept license agreements.
* feature: Option to automatically approve superseded updates of previously approved updates.
* feature: Option to automatically approve stale updates (designed to work alongside or replace the existing WSUS option as it can be used in conjunction with the license agreement option).
* feature: Test mode. Shows changes that would take place without actually doing them.
* feature: addition of ListGuids tool to aid in setting up the app.config.
* ui: now displays header.
* ui: now shows help listing when using /?.

###V1.0.0.0 (19 May 2009)

* Initial Release.
