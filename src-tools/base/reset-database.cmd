osql -E -S .\sqlexpress -d master -i sql/DropDatabases.sql
osql -E -S .\sqlexpress -d master -i sql/CreateDatabases.sql
build "db:migrate;db:sampledata"