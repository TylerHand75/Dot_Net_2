ECHO off

sqlcmd -S localhost -E -i MNCS_VAL_db.sql

rem server is localhost

ECHO .
ECHO if no errors appear DB was created
PAUSE