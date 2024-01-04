# Username Generator

<div align="center">

   Rust API built with axum that provides a random username.

![Logo](./.github/images/image.png)

[Development](#development-sparkles) •
[Roadmap](#roadmap-world_map) •
[FAQ](#faq-question) •
[Support](#support-love_letter)  

</div>

## Usage :zap:

- /username
  - Query parameters
    - `count` - Number of usernames to generate (default: 5)
    - `animals` - Bool to include animals in the username (default: true)
    - `adjectives` - Bool to include adjectives in the username (default: true)
    - `numbers` - Bool to include numbers in the username (default: false)
    - `nouns` - Bool to include nouns in the username (default: false)
    - `separator` - Separator between words in the username (default: "") **WIP**
  - Example
    - `http://localhost:3000/username?count=10&animals=false&nouns=true`

## Development :sparkles:

### Prerequisites :clipboard:

- [Rust](https://www.rust-lang.org/tools/install)

### Build :hammer:

```bash
cargo build
```

### Test :white_check_mark:

```bash
cargo test
```

### Run local :computer:

```bash
cargo run
```

or

```bash
.\hotreload.ps1
```

### Deploy :rocket:

*In progress*

## Roadmap :world_map:

- Support for separators
- Support for custom word lists
- Support for leet speak

## FAQ :question:

- How do I do X?
  - Just do Y!

## Support :love_letter:  

Submit an [issue!]()
