BLOB2File
=========

This is a small console program created in C# which dumps SQL Server BLOBs to files on the hard disk.

It assumes the following:
1. There is a column for BLOBs
2. The full filenames (with extensions) have their own column too
3. Both these columns reside in the same table

This code has been built using Visual Studio 2013 Express and used on Microsoft SQL Server 2008 R2 Express as well as 2012 Express.
