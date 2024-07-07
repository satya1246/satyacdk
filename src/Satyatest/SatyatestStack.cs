using Amazon.CDK;
using Constructs;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.SecretsManager;

namespace Satyatest
{
    public class SatyatestStack : Stack
    {
        internal SatyatestStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // The code that defines your stack goes here
              var ec2User = new User(this, "manual-ci-user");
              var accessKey = new AccessKey(this, "AccessKey", new AccessKeyProps { User = ec2User });
              var secret = new Secret(this, "Secret", new SecretProps {
               SecretStringValue = accessKey.SecretAccessKey});

            // Define an IAM policy for EC2 actions
            var ec2Policy = new PolicyStatement(new PolicyStatementProps
            {
                Actions = new[] { 
                    "ec2:RunInstances",
                    "ec2:DescribeInstances",
                    "ec2:TerminateInstances",
                    "ec2:StopInstances",
                    "ec2:StartInstances",
                    "ec2:RebootInstances"
                },
                Resources = new[] { "*" }
            });

            // Attach the policy to the user
            ec2User.AddToPolicy(ec2Policy);

            // Output the user's access key ID and secret access key
            // new CfnOutput(this, "EC2UserAccessKeyId", new CfnOutputProps
            // {
            //      Value = ec2User.accessKey.KeyId,
            //     Description = "Access Key ID for EC2 user (for initial setup)"
            // });

            // new CfnOutput(this, "EC2UserSecretAccessKey", new CfnOutputProps
            // {
            //     Value = ec2User.AccessKeys.ElementAt(0).SecretAccessKey,
            //     Description = "Secret Access Key for EC2 user (for initial setup)"
            // });
        }
    }
}
