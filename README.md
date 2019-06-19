Serko mail claim

The application is designed to run as a windows service, the requirement dictated that there should be a rest API that parses XML. This is done on the end point configured in the windows service configuration file.

The process is as follows

1) There is a timer currently set to poll the mailbox every 5 minutes, this timer will then download all emails from the mail address configured in mailConfig.xml. If you inspect the file you will find

username: serkomailclaim@gmail.com
password: Th!$!$$3cur3!

2) The mails are downloaded and checked for two criteria
	2.1) They have claim in the subject line
	2.2) The email message id has not been processed

If the criteria is met the mail is processed, firstly the body is stored as a blob in a mysql table titled request – the structure for which can be found in a liquibase project SerkoMailClaims/DbSourceControl see www.liquibase.com for documentation.

3) The mail message’s body is then sent to the rest service for processing. There was no requirement to retrieve a specifically       defined type. Hence I chose XmlDocument, this way the rest service does not need to know about the database, it only needs to know about this piece of string it is trying to create structure out of.

4) Once the XmlDocument is returned, the document is deserialised to dto’s and translated to dao objects contained in the datalayer project.

5) The Object is stored in the database in the table titled expense, again the structure for this table can be found in SerkoMailClaims/DbSourceControl.

6) The data is validated and a response email is sent back to the originating email address with the results from the validation.


Thank you for this test, this was challenging and made me think about the architecture and how this will be structured. I have attempted to include a drawing of the flow but it is a little tricky to get the flow diagram readable.

If there is anything you would like to know or additional details please dont hesitate to give me a shout.
