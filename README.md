# ISE 182 BGU Project - Group #8

"todo" comments in the code means uncompleted feature we need to implement.

What is left to do? (update this on the go)

- handling error/exceptions

- working with handlers of objects instead of objects themselves

- sorting of messages

- checking 150 characters of message before sending

- implementation of the logger (https://moodle2.bgu.ac.il/moodle/mod/forum/discuss.php?d=253650)

- adding option to "go back" on each menu (like I did on "register")

- now there are printings (WriteLine) from the logic layer ("Rigesteration was successful.." etc). We need to decide whether ALL printings should be from the presentation layer and there move all those printings there.

- updating the LLD & HLD

*Please write in whatsapp before you start coding something so we will know*

# Basic guideline for the code:
RunChat-->GUI-->Chat_EventHandler-->Chatroom-->anything else depends on the user's choice
