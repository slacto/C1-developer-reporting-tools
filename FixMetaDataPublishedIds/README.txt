We have a problem with import scripts for data types that includes IPublishControlled (probably just IPageMetaData in our solutions)

Problem is that Id MUST be the same in Published and Unpublished files. Otherwise we'll end up with dublicate rows in Published files! 
When the page is saved the IPageMetaData look for a match on Unpublished Id in Published files

Run script to fix all IPageMetaData