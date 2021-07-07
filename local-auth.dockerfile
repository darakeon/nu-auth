FROM local-dotnet
LABEL maintainer=""

COPY authorizer/Test /var/build/Test
COPY authorizer/Authorizer /var/build/Authorizer

RUN dotnet test /var/build/Test/Test.csproj
RUN dotnet publish /var/build/Authorizer/Authorizer.csproj -o /var/auth

RUN rm -r /var/build
RUN mkdir /var/file

CMD ./var/auth/Authorizer