# Screen

| Command                         | Info                                 |
| :------------------------------ | :----------------------------------- |
| `screen -S <name>`              | Start a new session with name <name> |
| `screen -ls`                    | List sessions                        |
| `screen -r <name>`              | Attach to session with name <name>   |
| `C-a, d`                        | Dettach from current session         |
| `screen -S <name> -X <command>` | Run command in new named session     |

```bash
pkill screen
screen -ls | grep '(Detached)' | awk 'sys {screen -S $1 -X quit}'
cd /home/macke/Services/Dev && screen -dmS dev Dev
cd /home/macke/Services/Blog && screen -dmS blog BlogService
cd /home/macke/Service/Tracker && screen -dmS tracker TrackerService
```
