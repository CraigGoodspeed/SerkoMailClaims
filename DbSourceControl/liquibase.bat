java -jar -Dfile.encoding=UTF-8 "..\Liquibase\liquibase.jar" --changeLogFile "databaseDefinition/masterLog.xml" --defaultsFile "mailclaims.properties" --classpath "..\Liquibase\lib\mysql-connector-java-5.1.31-bin.jar" --logLevel info update