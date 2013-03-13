---
title: Seting up the XML Configuration File
layout: default
---

##Seting up the XML Configuration File

Below is the default settings. These apply updates to all machines that need them, will approve stale updates, and approve any update that needs a license agreement.

*Note:* WSUS can be set to approve stale updates, the only stale updates it won't approve are those requiring a License Agreement.

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

###Detailed breakdown

####Server
Server lets you specify the connection settings for a server, so you can connect to a remote or local instance of WSUS.
<table class="table table-striped table-bordered table-condensed">
<thead>
<tr>
<th>Arguement Name</th>
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

####NoRunSet
This is run when you don't specify a /norunset on the command line.  This follows the "apply to all" approach of V1.0.0.0, the difference being that you can specify what is applied.

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

####RunSets
New to V1.0.0.1 is the ability to specify a runset via the command line.  This allows you to set up approval settings that can be run at different times (For example if you only want to check for a specific target group, or a certain type of update)

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

####TargetGroups
TargetGroups are the TargetGroups defined in WSUS, the config requires a GUID which can be obtained by the ListGuids tool.

####Classifications
Classifications match the Classifications defined in WSUS, the config requires a GUID which can be obtained by the ListGuids tool.  It also takes the same 4 arguments regarding the approval of updates.
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

####Products (new in V1.0.0.2)
Products match the Product Categories defined in WSUS, the config requires a GUID which can be obtained by the ListGuids tool.  If the products section is missing the classification rules will be applied to ALL products.
