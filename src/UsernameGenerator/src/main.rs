mod components;

use axum::{
    extract::Query,
    routing::get,
    http::StatusCode,
    Json,
    Router,
};
use components::{get_animal, get_adjective, get_nouns, get_cool_numbers};
use serde::{Deserialize};
use serde_with::{serde_as};

#[tokio::main]
async fn main() {
    tracing_subscriber::fmt::init();

    let app = Router::new()
        .route("/", get(get_username));

    let listener = tokio::net::TcpListener::bind("127.0.0.1:5004").await.unwrap();
    axum::serve(listener, app).await.unwrap();
}

async fn get_username(Query(params): Query<Params>) -> Result<Json<Vec<String>>, StatusCode> {
    let count = params.count;
    let mut components = vec![];

    if params.adjectives {
        components.push(get_adjective(count));
    }

    if params.animals {
        components.push(get_animal(count));
    }

    if params.nouns {
        components.push(get_nouns(count));
    }

    if params.cool_numbers {
        components.push(get_cool_numbers(count));
    }

    let usernames = (0..count)
        .map(|i| components.iter().map(|c| c[i].clone()).collect())
        .collect();

    Ok(Json(usernames))
}

fn default_true() -> bool {
    true
}

fn default_count() -> usize {
    5
}

#[serde_as]
#[derive(Debug, Deserialize)]
struct Params {
    #[serde(default = "default_count")]
    count: usize,

    #[serde(default = "default_true")]
    adjectives: bool,

    #[serde(default = "default_true")]
    animals: bool,

    #[serde(default)]
    nouns: bool,

    #[serde(default)]
    cool_numbers: bool,
}
