<?
$to = 'outwar.typpo@gmail.com';
$from = 'submit@typpo.us';
$name = 'Typpo DCT Mailer';
$subject = 'Typpo DCT Report/Request';

$message = stripslashes($_POST['message']);
$email = stripslashes($_POST['email']);
$identifier = stripslashes($_POST['identifier']);
$hide = stripslashes($_POST['hide']);

if ($message != NULL)
{
	if (strlen($message) < 10)
	{
		echo "Your submission is too short.";
		return;
	}

	if ((strpos($message, "autoreported") !== false && strpos($message, "3.1.24") === false) || strpos($message, "<title>Outwar.com Account Management</title>") !== false || strpos($message, "<title>Outwar - Web Based 2D Role Playing Game.</title>") !== false || strpos($message, "ByteCap") !== false)
	{
		echo "Input successful.";
		return;
	}

	if ($identifier)
	{
		$message = "Identifier: ".$identifier."\n\n".$message;
	}

	$headers = "From: $name <$from>";

	if ($email != NULL)
		$message .= "\n\nResponse email: ".$email;

	$ok = mail($to, $subject, $message, $headers);

	if($ok) 
	{
	    echo "<!-- Input successful. --><strong>Sent.</strong><br/><br/>";
	}
	else
	{
	    die("<strong>Not sent.  Something went wrong?</strong><br/><br/>");
	}

	if ($hide)
	{
		return;
	}
}
?>
<html>
<head>
<title>typpo.us - Typpo's Software - Outwar DC DCA DCAA auto attacker and more</title>
<style>
table {
	border-top-width: 1;
	border-left-width: 1;
	border-bottom-width: 1;
	border-right-width: 1;
	border-top-style: solid;
	border-left-style: solid;
	border-bottom-style: solid;
	border-right-style: solid;
}
body {
	font-family: Verdana, sans-serif;
}
</style>
</head>

<body>
<center><h2>DCT Bug/Feature Submissions</h2></center>

<table width="90%" align="center" cellspacing="9">
<td>
<form method="post" enctype="multipart/form-data">
Thank you for taking the time to submit a <strong>bug report</strong> or <strong>feature request</strong> for consideration.  The DCT has been around for 3 years now and we are still looking for ways to improve it.
<img src="/dct/screenshot.jpg" width="160" height="111" align="right" hspace="30" vspace="30"/>
<br/><br/>
If you are submitting a <strong>bug report</strong>, please include detailed, relevant information that makes it possible to <i>reproduce</i> the error or behavior.
<br/><br/>
If you are submitting a <strong>feature request</strong>, please be descriptive and understand that not everything can be included.
<br/><br/>
If you include your email address, you may hear back.
<br/><br/>
<textarea name="message" rows="20" cols="75"></textarea><br/>
Email (optional): <input type="text" name="email"/>
<br/><br/>
<input type="submit" value="Submit"/>
</form>
</td>
</table>
</body>
</html>
