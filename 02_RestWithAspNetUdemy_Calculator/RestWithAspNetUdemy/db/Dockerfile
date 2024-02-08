FROM mysql:5.7.22
EXPOSE 3306
COPY ./RestWithAspNetUdemy/DB/Migrations/ /home/database/
COPY ./RestWithAspNetUdemy/DB/DataSet/ /home/database/
COPY ./RestWithAspNetUdemy/CI/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh