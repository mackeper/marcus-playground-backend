#!/bin/bash

clear_output_directory() {
    output_directory=$1
    if [ -d "$output_directory" ]; then
        rm -rf $output_directory/*
    fi
}

publish_project() {
    project_path=$1
    output_directory=$2

    project_name=$(basename $project_path)
    output_path=$output_directory/$project_name
    echo "Publishing project $project_name"
    dotnet publish $project_path -c Release -r linux-x64 -o $output_path /p:DebugType=None /p:DebugSymbols=false /p:PublishSingleFile=true
    echo "Project $project_name published to $output_path"

    # Publish to cloud
    # scp -r $output_path/* gcp:~/Services/$project_name
}

main() {
    output_directory="./out/"
    project_paths=(
        "./src/Dev/Dev/"
        "./src/Blog/BlogService/"
        "./src/Identity/"
    )
    echo "Clearing output directory..."
    clear_output_directory $output_directory
    echo "Publishing projects..."
    for project_path in ${project_paths[@]}; do
        publish_project $project_path $output_directory
    done
    echo "Script execution completed."
}

main
