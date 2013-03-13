# WSUS Smart Approve

## Introduction

Introduction
WSUS Smart Approve is a tool for Microsoft Windows Server Update Service. Its purpose to approve updates as they are detected as being needed. The reason for this approach is for IT departments that want to deploy all needed updates automatically, but don't want to download every single update off Microsoft as they now pass (as of March 2009) 40GB across 26k updates in the English language alone!

Only a small portion of these will ever be needed, so allowing a machine to report what updates it is lacking and then approving them is a better use of disk space, bandwidth and time. Usually this approach would require someone to periodically check the WSUS administration control panel and approve downloads. This tool removes the need for this by automating this checking process.

This tool has been tested with WSUS 3.0 (all service packs) and Microsoft System Center Essentials 2010.

## Viewing the documentation

The documentation can be found at http://dhgms-solutions.github.com/wsussmartapprove/

## Contributing to the code

### 1\. Fork the code

See the [github help page for instructions on how to create a fork](http://help.github.com/fork-a-repo/).

### 2\. Apply desired changes

Use your preffered method for carrying out work.

### 3\. Send a pull request

See the [github help page for instructions on how to send pull requests](http://help.github.com/send-pull-requests/)