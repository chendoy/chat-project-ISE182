# ISE 182 BGU Project - Group #8


What is left to do? (update this on the go)

- handling error/exceptions - wrong key/user name doesn't exist/server not reachable... (sundy+naor)

- sorting of messages - need to test on bgu server (sundy)

- [V] implementation of the logger - in progress, log.txt not created (chen) 

- adding option to "go back" on each menu (like I did on "register") (sundy)

- [V] more elegant use of Chatroom instance (naor)

- reviewing the access modifiers in the code (private/public/protected) (chen)

- make sure PL and BL are completely divided (chen)

- make sure saving location is exist, maybe use of relative path (naor)

- updating the LLD & HLD (sundy)

*Please write in whatsapp before you start coding something so we will know*

# Basic guideline for the code:
RunChat-->GUI-->GUI_EventHandler-->Chat_EventHandler-->Chatroom-->anything else depends on the user's choice
RunChat: initiate startup
GUI: Responsible for the menus

GUI_EventHandler: Responsible for processing the user choice

Chat_EventHandler: Responsible for the initial processing of the input

Chatroom: Responsible for the final processing of the user input

