//this is a quick demo of how to connect to make an http server.
//not recommended in use in production.
#include <stdio.h>
#include <sys/socket.h> 
#include <arpa/inet.h>
#include <string.h>
#include <stdlib.h>
#include <unistd.h>
#include <netdb.h>
#include <ctype.h>
//a quick check to see if an input is an ip address
int is_ip_address(const char * str){
  if(str == NULL){
    return 0;
  }
  int i=0;
  int len = strlen(str);
  for(i=0;i<len;i++){
    if(!(isdigit(str[i]) || str[i] == '.')){
      return 0;      
    }
  }
  return 1;
}
void resolve(const char * name,char * ip){
  if(ip == NULL || name == NULL){
    return;
  }
  struct hostent * remote_host = gethostbyname(name);
  if(remote_host == NULL){
    ip = NULL;
    return;
  }
  fprintf(stdout,"Resolved hostname %s\n",remote_host->h_name);
  struct in_addr addr;
  if(remote_host->h_addrtype == AF_INET){
    addr.s_addr = *(unsigned long *) remote_host->h_addr_list[0];
    fprintf(stdout,"%s has ip address of %s\n",name,inet_ntoa(addr));
    char * resolved_ip = inet_ntoa(addr);
    strncpy(ip,resolved_ip,strlen(resolved_ip));
    //free(resolved_ip);
  }
}
int main(int argc , char *argv[])
{
  if(argc <3){
    fprintf(stderr,"usage: %s servername/ipaddress port\n",argv[0]);
    return 1;
  }
  int REPLY_LEN = 2001;
  char * reply = (char *)malloc(sizeof(char)*REPLY_LEN);
  char * message = NULL;
  char * ip = (char *)malloc(sizeof(char)*16);
  char * port = (char *)malloc(sizeof(char)*6);
  char * hostname = NULL;
  int socket_descriptor;
  struct sockaddr_in server; 
  int ip_len = strlen(argv[1]);
  if(ip_len>15){
    ip_len = 15;
  }
  int port_len = strlen(argv[2]);
  if(port_len>5){
    port_len = 5;
  }
  if(!is_ip_address(argv[1])){
    hostname = (char *)malloc(sizeof(char)*1001);
    strncpy(hostname,argv[1],1000);
    hostname[1000]=0;
    resolve(hostname,ip);
    if(ip == NULL){
      fprintf(stdout,"could not resolve %s\n",hostname);
      return 1;
    }
  }else{
    strncpy(ip,argv[1],ip_len);
  }
  strncpy(port,argv[2],port_len);
  socket_descriptor = socket(AF_INET , SOCK_STREAM , 0);
     
  if (socket_descriptor == -1){
    fprintf(stderr,"Could not create socket");
  }
  server.sin_addr.s_addr = inet_addr(ip);
  server.sin_family = AF_INET;
  server.sin_port = htons(atoi(port));
  
  fprintf(stdout,"%s\n",ip);
  fprintf(stdout,"%s\n",port);
  
  int connected = connect(socket_descriptor,(struct sockaddr *)&server,sizeof(server));
  if(connected < 0){
    fprintf(stderr,"Could not connect to %s on %s\n",ip,port);
    return 1;
  }
  fprintf(stdout,"Connected to %s on %s\n",ip,port);
  message = "GET / HTTP/1.1 \n\r";
  int sent = send(socket_descriptor,message,strlen(message),0);
  if(sent<0){
    fprintf(stderr,"error sending message\n");
    return 1;
  }
  fprintf(stdout,"Message sent\n");
  int recvd = 0;
  do{
    recvd = recvfrom(socket_descriptor,reply,REPLY_LEN,0,NULL,NULL);
    reply[recvd]=0;
    fprintf(stdout,"%s\n",reply);
  }while(recvd>0);
  close(socket_descriptor);
  free(ip);
  free(port);
  return 0;
}
