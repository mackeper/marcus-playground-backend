cargo build --release --target x86_64-unknown-linux-musl 

ssh gcp "mkdir -p ~/Services/UsernameGenerator"
scp ./target/x86_64-unknown-linux-musl/release/username_generator gcp:~/Services/UsernameGenerator/UsernameGeneratorService
ssh gcp "chmod +x ~/Services/UsernameGenerator/UsernameGeneratorService"

