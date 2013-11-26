BLOB2File
=========

This is a small console program created in C# which dumps SQL Server BLOBs to files on the hard disk.

Assumptions
-----------
* There is a column for BLOBs
* The full filenames (with extensions) have their own column too
* Both these columns reside in the same table

 
How it works
------------
* The SqlDataReader picks up the row
* The BLOB is fed into a byte array
* This byte array alongwith the filename (from the reader again) is passed to a method that writes the file
* The path is specified in the method that writes the file
* Once the file stream is closed, the reader moves to the next row and the cycle repeats.




This code has been built using Visual Studio 2013 Express and used on Microsoft SQL Server 2008 R2 Express as well as 2012 Express.
