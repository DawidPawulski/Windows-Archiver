# SanityArchiver
C# | .NET Framework | Windows Presentation Foundation. WPF application to archieve and handle files, encrypting and compressing text files.

## Description
As a User I would like to have an application in order to archive and handle my files.
### What I need:
* I would like to browse my files on drive
* I would like to compress a selected file
* I would like to crypt a selected file
* I would like to change the file attributes in a dialog

### Feature set:
* Files can be browsed, a file can be selected
* Compressing and decompressing of files are possible
* Encryption and decryption of files are possible
* File attributes can be changed
* File searching with regular expressions
* Text files can be opened with UTF character set
* Files can be copied and moved from here to there
* Calculate and show occupied space of directories

# Process Note

WPF application to list all running processes with the option to add comments to them.

## Table of contents
* [General info](#general-info)
* [Features](#features)
* [Opening the project](#Opening-the-project)
* [Technologies](#technologies)
* [Status](#status)

## General info
Windows Presentation Foundation application that allows user to check what kind of processes are running on the machine. User can also make comment on it. User can get the running process names and if he selects one of it he would see some major property of it.
These are:
* CPU usage
* Memory usage
* Running time
* Start time
* Threads of it in another dialog.

User can also comment textbox to left some note on it.

## Features
* Application can be started and stopped normally
* Notifies user to remember that his comments are not saved
* List processes, presented by PID + Name
* Show required attributes when user selects a process
* By double clicking refresh these attributes
* New dialog to show the threads of the process
* Comment saving in memory
* Always on the top function is selectable

## Opening the project
Open the project in VS Code. This is a .NET project.

## Technologies
* C#
* .NET Framework
* Windows Presentation Foundation
* Threads

## Status
Project is finished.


Display all directories and files
Encrypt and decrypt text files
Copy and move files
Search for a file by input (text or regex)
Show, modify file attributes
Compress and decompress files
