# brazil-api-integration

Simple api to search for cnpj and search for books, integrating with external public api service https://brasilapi.com.br/

- When opening the project, wait for the dependencies to load;
- To run the project locally, click on the project launch button, in visual studio.
- Install Kubctl (https://kubernetes.io/docs/tasks/tools/)
- Install minikube (https://minikube.sigs.k8s.io/docs/start/)

# Right click on the project, open in terminal, type cd.. + enter, and type the commands below:

## Generate application image locally
    docker build -f Brazil.Api.Integration\Dockerfile -t pos.puc.tcc.api .

## Run application container locally
    docker container run -d -p 49168:80 --name pos.puc.tcc.api pos.puc.tcc.api

## Push backend image to docker hub
	docker login
	docker tag pos.puc.tcc.api suaconta/pos.puc.tcc.api
	docker push suaconta/pos.puc.tcc.api

## Deploy in kubernetes
	kubectl create -f api-deploy.yaml
	
## Adjust auto scalling
	kubectl autoscale deployment pos-puc-tcc-api --min=1 --max=6 --cpu-percent=50
	
## Adjust replicas manually
	kubectl scale deployment/pos-puc-tcc-api --replicas=5

## General commands kubernetes
	kubectl get all
	kubectl get deployments
	kubectl get svc
	kubectl get pods
	kubectl get services
	kubectl get deploy
	kubectl get nodes		
	kubectl delete services --all
	kubectl delete pod pos-puc-tcc-front
	kubectl delete node name-node
	kubectl describe deploy pos-puc-tcc-front
	kubectl describe svc pos-puc-tcc-front

# DATA FOR TEST

## Books: 
- 9788545702870  Akira vol. 1
- 9788535919714  Steve Jobs
- 9788562936524  A guerra dos tronos
- 9788599296363  A cabana
- 9788525432186  Sapiens
- 9788539004119  O poder do hábito
- 9788551002490  A Sutil Arte de Ligar o F*da-se
- 9786584661097  365 Dias de Inteligência
- 9786559223503  Autismo
- 9788576849940  O milagre da manhã 
- 9788502218482  Ansiedade
- 9788543104508  Propósito
- 9788545202219  O poder da autorresponsabilidade 
- 9788562409882  A garota do lago

## Companies:
- 17178195002968  puc minas
- 06990590000123  google
- 04712500000107  microsoft
- 13590585000199  netflix
- 57286247000133  intel
- 15436940000103  amazon