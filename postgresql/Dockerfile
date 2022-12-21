# This docker file download latest postgres alpine (for minimal size)
# and copies files to run on initialization, these scripts are necessary for orleans persistance
FROM postgres:15.1-alpine
COPY /PostgreSQL-Main.sql /docker-entrypoint-initdb.d/01.sql
COPY /PostgreSQL-Clustering.sql /docker-entrypoint-initdb.d/02.sql
COPY /PostgreSQL-Persistence.sql /docker-entrypoint-initdb.d/03.sql
COPY /PostgreSQL-Reminders.sql /docker-entrypoint-initdb.d/04.sql
COPY /PostgreSQL-Clustering-3.7.0.sql /docker-entrypoint-initdb.d/05.sql