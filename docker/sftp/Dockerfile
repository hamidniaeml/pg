# docker build -f Dockerfile -t sftp_image .
# docker run -p 2222:22 -it --rm --name run_sftp sftp_image
# sftp -oPort=2222 -i ./dev_sftp_pkey sshuser@127.0.0.1
# docker exec -it run_sftp bash
# docker kill run_sftp

FROM emlauacr.azurecr.io/infra-base-ubuntu-2204:latest
RUN apt install openssh-server sudo -y
RUN apt install less nano sudo -y
RUN groupadd sshgroup 
RUN useradd -ms /bin/bash -g sshgroup sshuser
RUN echo "sshuser:pass" | chpasswd
RUN mkdir -p /home/sshuser/.ssh
COPY dev_sftp_pkey.pub /home/sshuser/.ssh/authorized_keys
RUN chown sshuser:sshgroup /home/sshuser/.ssh/authorized_keys 
RUN chmod 600 /home/sshuser/.ssh/authorized_keys
RUN echo 'Match User sshuser' >> /etc/ssh/sshd_config
RUN echo 'AuthorizedKeysFile .ssh/authorized_keys' >> /etc/ssh/sshd_config
COPY dev_clearing_test_file /home/sshuser/

EXPOSE 22
ENTRYPOINT service ssh restart && bash && cd /home/sshuser