---
title: Home
layout: default
---

<h2>Introduction</h2>
<p>WSUS Smart Approve is a tool for Microsoft Windows Server Update Service. Its purpose to approve updates as they are detected as being needed. The reason for this approach is for IT departments that want to deploy all needed updates automatically, but don't want to download every single update off Microsoft as they now pass (as of March 2009) 40GB across 26k updates in the English language alone!</p>
<p>Only a small portion of these will ever be needed, so allowing a machine to report what updates it is lacking and then approving them is a better use of disk space, bandwidth and time. Usually this approach would require someone to periodically check the WSUS administration control panel and approve downloads. This tool removes the need for this by automating this checking process.</p>
<p>This tool has been tested with WSUS 3.0 (all service packs) and Microsoft System Center Essentials 2010.</p>
<h2>Getting Started</h2>
<h3>Downloading WSUS Smart Approve</h3>
<div class="alert">
<p>If you downloaded the WSUS Smart Approve from somewhere other than the <a href="http://wsussmartapprove.codeplex.com">Codeplex project</a> please check your download.  There are various mirrors on advert driven download sites who are hosting a copy without our involvement, due to the license they're not required to get our permission. Codeplex is the only site where we have control of the contents of the download, we can not guarantee modifications, malicious or otherwise have not been carried out on these mirrors.</p>
</div>
<p><a href="http://wsussmartapprove.codeplex.com/releases/">WSUS Smart Approve is available from CodePlex</a></p>
<h3>Configuring WSUS Smart Approve</h3>
<p>See the <a href="/wsussmartapprove/instructions/">instructions on how to configure and use WSUS Smart Approve</a>.</p>
<h3>Working with the source code and project documentation</h3>
<p>See the <a href="/wsussmartapprove/sourcecode/">instructions on how to use the source code, and contribute</a>.</p>
<p>See the <a href="/wsussmartapprove/documentation/">instructions on how to contribute to the project documentation</a>.</p>
<h2>Limitations</h2>
<ul>
  <li>You must be an administrator in order to run this tool</li>
  <li>The delay between an update being reported as being needed, being approved and downloaded, and being installed on the client depends on:
    <ul>
      <li>How often the client checks WSUS (group policy or registry setting).</li>
      <li>How long after the detection WSUS Smart Approve is run.</li>
      <li>How large the download is.</li>
    </ul>
  </li>
</ul>
<h2>Roadmap</h2>
<p>Coming soon</p>
<h2>Future Possibilities</h2>
<ul>
  <li>Tool to remotely connect to computers and tell them to check WSUS again</li>
  <li>Installer</li>
  <li>Configuration Tool</li>
  <li>Reporting to email, database or web system</li>
</ul>