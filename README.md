# ISE 182 BGU Project - Group #8

"todo" comments in the code means uncompleted feature we need to implement.

What is left to do? (update this on the go)

- handling error/exceptions

- sorting of messages - need to test on bgu server

- implementation of the logger (https://moodle2.bgu.ac.il/moodle/mod/forum/discuss.php?d=253650) - in progress, log.txt not created

- adding option to "go back" on each menu (like I did on "register"), need consider if still needed and where

- 'switch user' option in User menu if needed

- more elegant use of Chatroom instance

- make sure PL and BL are completely divided

- make sure saving location is exist, maybe use of relative path

- updating the LLD & HLD

*Please write in whatsapp before you start coding something so we will know*

# Basic guideline for the code:
RunChat-->GUI-->GUI_EventHandler-->Chat_EventHandler-->Chatroom-->anything else depends on the user's choice
RunChat: initiate startup
GUI: Responsible for the menus
GUI_EventHandler: Responsible for processing the user choice
Chat_EventHandler: Responsible for the initial processing of the input
Chatroom: Responsible for the final processing of the user input

