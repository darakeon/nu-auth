FROM ubuntu:20.04
LABEL maintainer=""

RUN echo '#!/bin/bash' > /bin/maintain
RUN echo 'apt upgrade -y' >> /bin/maintain
RUN echo 'apt update' >> /bin/maintain
RUN echo 'apt autoremove -y' >> /bin/maintain
RUN echo 'apt clean' >> /bin/maintain
RUN chmod +x /bin/maintain

RUN maintain

RUN apt install -y curl nano unzip

RUN echo 'export PS1="\n\n[\[\033[01;30m\]\A\[\033[00m\]] \[\033[01;31m\]\u\[\033[00m\]@\[\033[01;35m\]\W\[\033[00m\]$ "' >> ~/.bashrc

RUN apt install -y ca-certificates
RUN curl https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb > packages-microsoft-prod.deb \
	&& dpkg -i packages-microsoft-prod.deb \
	&& rm packages-microsoft-prod.deb

RUN apt update
RUN apt install -y apt-transport-https
RUN apt install -y dotnet-sdk-5.0
RUN apt install -y aspnetcore-runtime-5.0

RUN echo "echo" >> ~/.bashrc
RUN echo "printf '\e[38;5;46m'" >> ~/.bashrc
RUN echo "echo --------------------------------------------------------------------------------" >> ~/.bashrc
RUN echo "echo ------------------------------------ DOTNET ------------------------------------" >> ~/.bashrc
RUN echo "echo --------------------------------------------------------------------------------" >> ~/.bashrc
RUN echo "printf '\e[38;5;51m'" >> ~/.bashrc
RUN echo "dotnet --version" >> ~/.bashrc
RUN echo "printf '\e[38;5;253m'" >> ~/.bashrc

RUN mkdir /var/auth
WORKDIR /var/auth

CMD bash
