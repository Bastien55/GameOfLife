pipeline {
    agent any
    stages{
        stage ('Git Checkout') {
            steps {
                cleanWs()
                git branch: 'Développement', url: 'https://github.com/Bastien55/GameOfLife.git'
            }
        }

        stage('Restore packages') {
            steps {
                bat "dotnet restore ${workspace}\\GameOfLife.csproj"
            }
        }
        
        stage ('Build') {
            steps {
                bat "dotnet build ${workspace}\\GameOfLife.csproj"
            }
        }
    }
}