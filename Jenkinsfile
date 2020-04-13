
pipeline {
 agent any
 stages {
  stage('Checkout') {
   steps {
    git credentialsId: '9bc2b01b-65f6-4bc0-933a-cfba9606ef51', url: 'https://github.com/hkamran2/Books.git', branch: 'jenkins-build'
   }
  }
  stage('Restore') {
   steps {
    sh "dotnet restore"
   }
  }
  stage('Build') {
   steps {
    sh 'dotnet build --configuration Release'
   }
  }
 }
}