FROM microsoft/dotnet:2.2-sdk-alpine AS build
COPY . .

WORKDIR /templates/service/host/AuthServerHost
RUN dotnet restore
RUN dotnet ef migrations script -i -o migrations-AuthServerHost.sql

WORKDIR /templates/service/host/HQSOFT.eBiz.AccountPayable.Host
RUN dotnet restore
RUN dotnet ef migrations script -i -o migrations-AccountPayable.sql

FROM mcr.microsoft.com/mssql-tools AS final
WORKDIR /src
COPY --from=build /templates/service/host/AuthServerHost/migrations-AuthServerHost.sql migrations-AuthServerHost.sql
COPY --from=build /templates/service/host/HQSOFT.eBiz.AccountPayable.Host/migrations-AccountPayable.sql migrations-AccountPayable.sql
COPY --from=build /templates/service/database/entrypoint.sh .
RUN /bin/bash -c "sed -i $'s/\r$//' entrypoint.sh"
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]