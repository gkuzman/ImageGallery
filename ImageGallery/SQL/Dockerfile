FROM mcr.microsoft.com/mssql/server
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=MyPassword001

WORKDIR /tmp
COPY setup.sql .

RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 \
	&& /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "MyPassword001" -i /tmp/setup.sql \
	&& pkill sqlservr