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
screen -S dev -X "./home/macke/Services/Dev/DevService"
screen -S dev -X "./home/macke/Services/Blog/BlogService"
screen -S dev -X "./home/macke/Services/Tracker/TrackerService"
```
