use rand::seq::IteratorRandom;

pub fn get_animal(count: usize) -> Vec<String> {
    let content = include_str!("animals.txt");
    get_count_random(content.to_string(), count)
}

pub fn get_adjective(count: usize) -> Vec<String> {
    let content = include_str!("adjectives.txt");
    get_count_random(content.to_string(), count)
}

pub fn get_nouns(count: usize) -> Vec<String> {
    let content = include_str!("nouns.txt");
    get_count_random(content.to_string(), count)
}

pub fn get_cool_numbers(count: usize) -> Vec<String> {
    let content = include_str!("cool_numbers.txt");
    get_count_random(content.to_string(), count)
}

fn get_count_random(content: String, count: usize) -> Vec<String> {
    let lines: Vec<&str> = content.lines().collect();
    let mut rng = rand::thread_rng();
    (0..count)
        .map(|_| lines.iter().choose(&mut rng).unwrap().to_string())
        .collect()
}
