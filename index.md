<!DOCTYPE html>
<html lang="en">
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
		<title>{{ page.title }} - WSUS Smart Approve Documentation</title>
		<link rel="stylesheet" href="/css/bootstrap.css" type="text/css" />
		<link rel="stylesheet" href="/css/font-awesome.css" type="text/css" />
		<link rel="stylesheet" href="/css/site.css" type="text/css" />
		<link href="/css/prettify.css" type="text/css" rel="stylesheet" />
		<script type="text/javascript" src="/js/prettify.js"></script>
	</head>
	<body onload="prettyPrint()">
		<div id="contentcontainer">
       <div class="header">
         <div class="headerinner">
           <div id="floatcontainer">
             <h1>WSUS Smart Approve Documentation</h1>
           </div>
         </div>
       </div>
       <div id="maincontent">
           <div class="container-fluid">
               <div class="row-fluid">
                   <div class="span10">
<h2>Table of contents</h2>
<ol>
  <li><a href="#introduction">Introduction.</a></li>
  <li><a href="#downloading">Downloading WSUS Smart Approve.</a></li>
  <li>
    <a href="#configuring">Configuring WSUS Smart Approve.</a>
    <ol>
      <li><a href="#configxml">Configuring the XML file.</a></li>
      <li><a href="#configtask">Configuring a scheduled task.</a></li>
    </ol>
  </li>
  <li><a href="#sourcecode">Working with the source code</a></li>
  <li><a href="#documentation">Contributing to the documentation</a></li>
  <li>
    <a href="#history">Project history</a>
    <ol>
      <li><a href=#implemented">Implemented.</a></li>
      <li><a href=#roadmap">Road Map.</a></li>
    </ol>
  </li>
  <li><a href="#credits>Credits</a></li>

<a id="introduction" /><h2>Introduction</h2>
<p>WSUS Smart Approve is a tool for Microsoft Windows Server Update Service. Its purpose to approve updates as they are detected as being needed. The reason for this approach is for IT departments that want to deploy all needed updates automatically, but don't want to download every single update off Microsoft as they now pass (as of March 2009) 40GB across 26k updates in the English language alone!</p>
<p>Only a small portion of these will ever be needed, so allowing a machine to report what updates it is lacking and then approving them is a better use of disk space, bandwidth and time. Usually this approach would require someone to periodically check the WSUS administration control panel and approve downloads. This tool removes the need for this by automating this checking process.</p>
<p>This tool has been tested with WSUS 3.0 (all service packs) and Microsoft System Center Essentials 2010.</p>

<a id="downloading" /><h2>Downloading WSUS Smart Approve</h2>
<div class="alert">
    <p>If you downloaded the WSUS Smart Approve from somewhere other than the <a href="http://wsussmartapprove.codeplex.com">Codeplex project</a> please check your download.  There are various mirrors on advert driven download sites who are hosting a copy without our involvement, due to the license they're not required to get our permission. Codeplex is the only site where we have control of the contents of the download, we can not guarantee modifications, malicious or otherwise have not been carried out on these mirrors.</p>
</div>
<p><a href="http://wsussmartapprove.codeplex.com/releases/">WSUS Smart Approve is available from CodePlex</a>.</p>

<a id="configuring" /><h2>Configuring WSUS Smart Approve.</h2>

<p>WSUS Smart Approve is a console application that is intended to be run on a regular basis, i.e. as a scheduled task.  The more frequently it is run, the less time there will be between an update being reported as needed, and it being approved for download.</p>

<p>In order to carry out these tasks you need to have administrator access.  The login which you choose to run WSUS Smart Approve under must have administrator access.  You can add an account to your domain specifically for this tool, for now I will leave a search engine as your source for instructions on how to do this.</p>

<p>WSUS Smart Approve is a console application that is intended to be run on a regular basis, i.e. as a scheduled task.  The more frequently it is run, the less time there will be between an update being reported as needed, and it being approved for download.</p>

<p>In order to carry out these tasks you need to have administrator access.  The login which you choose to run WSUS Smart Approve under must have administrator access.  You can add an account to your domain specifically for this tool, for now I will leave a search engine as your source for instructions on how to do this.</p>

<a id="configwsus" /><h3>Configuring WSUS.</h3>

<p>WSUS needs to be set to detect all the classifications and products you wish to be approved.  A pre-exisiting WSUS setup will most likely already be set to do this, you just need to stop it automatically approving everything.</p>

<ul>
<li>Open the WSUS MMC Console</li>
<li>Navigate to Update Services -> %SERVER NAME% -> Options</li>
<li>Ensure you have chosen the <b>Products and Classifications</b> that you want detected</li>
<li>Choose the Automatic Approvals option<li>
<li>Edit the <b>Default Automatic Approval Rule</b> and any other rule which may exist</li>
<li><b>Untick</b> any type of update you <b>do not want to be automatically downloaded</b></li>
<li>Note: If you just want to detect everything and not approve them automatically you want to delete the default rule (as it must have 1 option chosen at all times), this decision is down to you.</li>
</ul>

<a id="configxml" /><h3>Configuring the XML file.</h3>

<p>Below is the default settings. These apply updates to all machines that need them, will approve stale updates, and approve any update that needs a license agreement.</p>

<p><b>Note:</b> WSUS can be set to approve stale updates, the only stale updates it won't approve are those requiring a License Agreement.</p>

<pre class="prettyprint linenums"><code class="lang-xml">
	&lt;?xml version="1.0" encoding="utf-8" ?&gt;
	&lt;configuration&gt;
		&lt;configSections&gt;
			&lt;section name="ApplicationSettings" type="SmartApprove.Model.ApplicationSettings, smartapprove, Version=1.0.0.5" /&gt;
		&lt;/configSections&gt;
		&lt;ApplicationSettings&gt;
			&lt;Server Hostname="localhost" Port="8530" Secure="false" /&gt;
			&lt;NoRunSet AcceptLicenseAgreement="true" ApproveStaleUpdates="true" ApproveSupersededUpdates="true" ApproveNeededUpdates="true" /&gt;
		&lt;/ApplicationSettings&gt;
	&lt;/configuration&gt;
</code></pre>

For a more complex setup you can base your config upon:

<pre class="prettyprint linenums"><code class="lang-xml">
	&lt;?xml version="1.0" encoding="utf-8" ?&gt;
	&lt;configuration&gt;
		&lt;configSections&gt;
			&lt;section name="ApplicationSettings" type="SmartApprove.Model.ApplicationSettings, smartapprove, Version=1.0.0.5" /&gt;
		&lt;/configSections&gt;
		&lt;ApplicationSettings&gt;
			&lt;Server Hostname="localhost" Port="8530" Secure="false" /&gt;
			&lt;NoRunSet AcceptLicenseAgreement="true" ApproveStaleUpdates="true" ApproveSupersededUpdates="true" ApproveNeededUpdates="true" /&gt;
			&lt;RunSets&gt;
				&lt;add Name="Normal"&gt;
					&lt;TargetGroups&gt;
						&lt;!-- All Computers --&gt;
						&lt;add Guid="a0a08746-4dbe-4a37-9adf-9e7652c0b421"&gt;
							&lt;Classifications&gt;
								&lt;!-- Critical Updates --&gt;
								&lt;add Guid="e6cf1350-c01b-414d-a61f-263d14d133b4" AcceptLicenseAgreement="true" ApproveStaleUpdates="true" ApproveSupersededUpdates="false" ApproveNeededUpdates="true"&gt;
									&lt;Products&gt;
										&lt;!-- Windows --&gt;
										&lt;add Guid="6964aab4-c5b5-43bd-a17d-ffb4346a8e1d" /&gt;
									&lt;/Products&gt;
								&lt;/add&gt;
							&lt;/Classifications&gt;
						&lt;/add&gt;
					&lt;/TargetGroups&gt;
				&lt;/add&gt;
			&lt;/RunSets&gt;
		&lt;/ApplicationSettings&gt;
	&lt;/configuration&gt;
</code></pre>

<h4>Server</h4>
Server lets you specify the connection settings for a server, so you can connect to a remote or local instance of WSUS.
<table class="table table-striped table-bordered table-condensed">
<thead>
<tr>
<th>Argument Name</th>
<th>Type</th>
<th>Description</th>
</tr>
</thead>
<tr>
<td>Hostname</td>
<td>string</td>
<td>name of the server to connect to</td>
</tr>
<tr>
<td>Port</td>
<td>unsigned int</td>
<td>port to connect to</td>
</tr>
<tr>
<td>Secure</td>
<td>boolean</td>
<td>whether to use SSL or not</td>
</tr>
</table>

<h4>NoRunSet</h4>
This is run when you don't specify a /norunset on the command line.  This follows the "apply to all" approach of V1.0.0.0, the difference being that you can specify what is applied.

<table class="table table-striped table-bordered table-condensed">
<thead>
<tr>
<th>Argument Name</th>
<th>Type</th>
<th>Description</th>
</tr>
</thead>
<tr>
<td>AcceptLicenseAgreement</td>
<td>Boolean</td>
<td>Whether to accept a license agreement.  If you set this to false and an update needs a license accepting first, it won't be approved.</td>
</tr>
<tr>
<td>ApproveStaleUpdates</td>
<td>Boolean</td>
<td>Whether to approve new revisions of already approved updates.</td>
</tr>
<tr>
<td>ApproveSupersededUpdates</td>
<td>Boolean</td>
<td>Whether to approve an update that replaces a previous update, that has already been approved.</td>
</tr>
<tr>
<td>ApproveNeededUpdates</td>
<td>Boolean</td>
<td>Approve updates that are reported as needed.</td>
</tr>
</table>

<h4>RunSets</h4>
<p>New to V1.0.0.1 is the ability to specify a runset via the command line.  This allows you to set up approval settings that can be run at different times (For example if you only want to check for a specific target group, or a certain type of update)</p>

<table class="table table-striped table-bordered table-condensed">
<thead>
<tr>
<th>Arguement Name</th>
<th>Type</th>
<th>Description</th>
</tr>
</thead>
<tr>
<td>Name</td>
<td>String</td>
<td>Name of a runset.</td>
</tr>
</table>

<h4>TargetGroups</h4>
<p>TargetGroups are the TargetGroups defined in WSUS, the config requires a GUID which can be obtained by the ListGuids tool.</p>

<h4>Classifications</h4>
<p>Classifications match the Classifications defined in WSUS, the config requires a GUID which can be obtained by the ListGuids tool.  It also takes the same 4 arguments regarding the approval of updates.</p>
<table class="table table-striped table-bordered table-condensed">
<thead>
<tr>
<th>Arguement Name</th>
<th>Type</th>
<th>Description</th>
</tr>
</thead>
<tr>
<td>AcceptLicenseAgreement</td>
<td>Boolean</td>
<td>Whether to accept a license agreement .  If you set this to false and an update needs a license accepting first, it won't be approved.</td>
</tr>
<tr>
<td>ApproveStaleUpdates</td>
<td>Boolean</td>
<td>Whether to approve new revisions of already approved updates</td>
</tr>
<tr>
<td>ApproveSupersededUpdates</td>
<td>Boolean</td>
<td>Whether to approve an update that replaces a previous update, that has already been approved.</td>
</tr>
<tr>
<td>ApproveNeededUpdates</td>
<td>Boolean</td>
<td>Approve updates that are reported as needed.</td>
</tr>
</table>

<h4>Products (new in V1.0.0.2)</h4>
Products match the Product Categories defined in WSUS, the config requires a GUID which can be obtained by the ListGuids tool.  If the products section is missing the classification rules will be applied to ALL products.

<a id="configcommandline" /><h3>Working out the command line.</h3>

<p>The command line depends on how you have <a href="#configxml">configured the XML config</a>.  If you wish to use the <b>no runset</b> approach you use the command:</p>

	SmartApprove.exe /norunset

<p>If you wish to use the runset approach the command is</p>

	SmartApprove.exe /runset "name"

<p>If the name of the runset contains a space it *must* be surrounded by quotes.</p>

<p>If you wish to see how your configuration will be applied, without actually applying changes you can use the test mode which is as simple as adding /test onto the relevant command like so:</p>

	SmartApprove.exe /norunset /test
	SmartApprove.exe /runset "name" /test

<a id="configtask" /><h3>Configuring a scheduled task.</h3>

<ul>
<li>Navigate to Control Panel -> Administrative Tools -> Task Scheduler (On Windows Server 2003 and earlier the path is Control Panel -> Scheduled Tasks).</li>
<li>Choose *Add Scheduled Task*</li>
<li>Browse to the application and select it (SmartApprove.exe)</li>
<li>Add the relevant arguements described above</li>
<li>Initially choose Daily, as we are going to edit this</li>
<li>Stick with the default times (for now)</li>
<li>Choose the login you wish to run this with, it must have administrator access in order to work with WSUS.</li>
<li>Choose *Open advanced properties for this task when i click Finish*</li>
<li>Decide how often you want to have the tool run, say every 3 hours from 0600 (or a custom pattern of just after people get to work, lunchtime and just after they leave ;))</li>
</ul>

<h4>Using multiple schedules</h4>
<ul>
<li>Change the start time to 06:00</li>
<li>Check *Show multiple schedules*</li>
<li>Click the *New* button</li>
<li>Set a schedule of Daily and a time of 09:00</li>
<li>Repeat the addition for 12:00, 15:00, 18:00, 21:00, 00:00 and 03:00</li>
</ul>

<h4>Using a single schedule</h4>
<ul>
<li>Alternatively you can use a single schedule.</li>
<li>Set the initial time of say 06:00.</li>
<li>Click advanced.</li>
<li>Check *Repeat task*.</li>
<li>Choose a repeat interval, for example every 3 hours.</li>
<li>Set a duration of 23:55.</li>
</ul>

<a id="sourcecode" /><h2>Working with the source code.</h2>

<a id="documentation" /><h2>Contributing to the documentation.</h2>

<p>Contributions to the documentation are more than welcome, and the process is designed to be as simple as possible.</p>

<h3>Fork the documentation.</h3>

<p>See the <a href="http://help.github.com/fork-a-repo/">github help page for instructions on how to create a fork</a>.</p>

<h3>Write desired content.</h3>

<p>Use your preffered method for carrying out work.</p>

<h3>Send a pull request.</h3>

<p>See the <a href="http://help.github.com/send-pull-requests/">github help page for instructions on how to send pull requests</a></p>

<a id="history" /><h2>Project History.</h2>
<a id="implemented" /><h3>Implemented.</h3>
<ul>
<li><a href="#v1_0_2">V1.0.2</a></li>
<li><a href="#v1_0_1">V1.0.1</a></li>
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

<h3 id="v1_0_2">V1.0.2 (17 March 2013)</h3>
<ul>
  <li>Fixed backward compatability issue between WSUS 3.1 and Windows Server 2012 RSAT tools.</li>
</ul>

<h3 id="v1_0_1">V1.0.1 (10 March 2013</h3>
<ul>
  <li>Addition of contribution: Copy approvals between targetgroups.</li>
  <li>removed update scope filter on previously approved items, as it's stopping new target groups getting needed approvals.</li>
</ul>

<h3 id="v1_0_0_5">V1.0.0.5 (28 January 2012)</h3>

<ul>
  <li>Declined updates are no longer approved if they have a history of being needed.</li>
  <li>Changed the way the config file is checked for detecting AllClassifications or ProductClassification inside a runset.</li>
<ul>

<h3 id="v1_0_0_4">V1.0.0.4 (23 January 2012)</h3>

<ul>
  <li>Fix: Listguids crashing with Unauthorised access exception.</li>
  <li>Fix: version strings and assembly information was wrong.</li>
</ul>

<h3 id="v1_0_0_3">V1.0.0.3 (22 January 2012)</h3>

<ul>
  <li>Fix: problem where approved update name's still aren't appearing in console.</li>
  <li>Fix: Creating Install for Update that doesn't support install.</li>
</ul>

<h3 id="v1_0_0_2">V1.0.0.2 (22 March 2010)</h3>

<ul>
  <li>Fix: Update name no longer displayed in console.</li>
  <li>Fix: Stale Updates and Superseded logic now correctly filters by Classification (was applying all classifications regardless of config settings).</li>
  <li>New: create installer.</li>
  <li>New: Allow approval rules for specific product.</li>
  <li>Fix: Listguids and SmartApprove now check the user has the necessary permissions on WSUS.</li>
</ul>

<h3 id="v1_0_0_1">V1.0.0.1 (12 October 2009)</h3>

<ul>
  <li>feature: Added XML Config schema.</li>
  <li>feature: Ability to specify the server to connect to in the XML config.</li>
  <li>feature: Ability to specify runsets.</li>
  <li>feature: Added Command Line option for specifying which runset to perform.</li>
  <li>feature: Ability to specify rules for target groups and classifications.</li>
  <li>feature: Option to accept license agreements.</li>
  <li>feature: Option to automatically approve superseded updates of previously approved updates.</li>
  <li>feature: Option to automatically approve stale updates (designed to work alongside or replace the existing WSUS option as it can be used in conjunction with the license agreement option).</li>
  <li>feature: Test mode. Shows changes that would take place without actually doing them.</li>
  <li>feature: addition of ListGuids tool to aid in setting up the app.config.</li>
  <li>ui: now displays header.</li>
  <li>ui: now shows help listing when using /?.</li>
</ul>

<h3 id="v1_0_0_0">V1.0.0.0 (19 May 2009)</h3>

<ul>
<li>Initial Release.</li>
</ul>

<a id="roadmap" /><h3>Road Map.</h3>

<a id="credits" /><h2>Credits.</h2>
<h3>Main Project</h3>
<dl class="dl-horizontal">
<dt><a href="http://www.dpvreony.co.uk">David Vreony</a></dt>
<dd>Primary Development.</dd>
<dt>Petr Herzig</dt>
<dd>Feedback on more complex approval groups.</dd>
<dt>jeddytier4</dt>
<dd>Contribution on copying between target groups.</dd>
<dt>Yasufumi Shiraishi</dt>
<dd>Original author of the copying between target groups logic.</dd>
</dl>
<h3>Documentation Site</h3>
<ul>
<li><a href="http://fortawesome.github.com/Font-Awesome/">Font Awesome</a></li>
<li><a href="http://pages.github.com/">Github Pages</a></li>
<li><a href="http://code.google.com/p/google-code-prettify/">Google Prettify</a></li>
<li><a href="https://github.com/mojombo/jekyll">Jekyl</a></li>
<li><a href="http://daringfireball.net/projects/markdown/">Markdown</a></li>
<li><a href="http://twitter.github.com/bootstrap/">Twitter Bootstrap</a></li>
</ul>
</ol>
                   </div>
                   <div class="span2">
<li class="nav-header">Project Links</li>
  <li><a href="http://wsussmartapprove.codeplex.com" title="View the Codeplex project site for WSUS Smart Approve">Codeplex</a></li>
  <li><a href="http://www.dhgms.com/projects/wsussmartapprove" title="View the DHGMS Solutions project site for WSUS Smart Approve">DHGMS Solutions</a></li>
  <li><a href="http://www.dpvreony.co.uk/portfolio/project/v/24/" title="View the DPVreony portfolio details on WSUS Smart Approve">DPVreony</a></li>
  <li><a href="https://github.com/DHGMS-Solutions/wsussmartapprove" title="View the Github project site for WSUS Smart Approve">Github</a></li>
  <li><a href="http://www.ohloh.net/p/wsussmartapprove?ref=gitpages" title="View the ohloh project site for WSUS Smart Approve">ohloh</a></li>
</ul>
</nav>
<a href="http://www.ohloh.net/p/wsussmartapprove?ref=gitpages" title="View the project details on ohloh"><img alt="" src="http://www.ohloh.net/p/wsussmartapprove/widgets/project_partner_badge.gif" /></a>
                   </div>
               </div>
             </div>
         </div>
      </div>
    </div>
  </body>
</html>