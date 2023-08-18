`sudo apt update`

# Nginx
`sudo apt get install nginx`
`sudo nano /etc/nginx/sites-enabled/default`

```

```


# SSL (Letsencrypt + certbot)
https://www.youtube.com/watch?v=R5d-hN9UtpU
`sudo apt install snapd`
`sudo snap install core; sudo snap refresh core`

`sudo snap install --classic certbot`
`sudo ln -s /snap/bin/certbot /usr/bin/certbot`
`sudo certbot --version`
`sudo certbot --nginx --test-cert`
`sudo certbot --nginx`
`sudo certbot renew --dry-run`
