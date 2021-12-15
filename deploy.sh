LOGIN=$(aws ecr get-login --no-include-email --region us-east-1)
$LOGIN
docker push 323700164624.dkr.ecr.us-east-1.amazonaws.com/pco-accountcore-uat:latest
aws ecs update-service --cluster AccountCoreCluster --service PCO-ACCOUNTCORE-SVC --task-definition PCOAccountCoreWebAPI --force-new-deployment