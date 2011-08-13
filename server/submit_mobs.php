<?
$path = 'mobs.submitted';

$mobs = stripslashes($_POST['mobs']);

if ($mobs != NULL)
{
	if (strlen($mobs) < 1)
	{
		return;
	}

	$fh = fopen($path, 'a') or die('can\'t open file - a');
	fwrite($fh, $mobs);
	fclose($fh);
}
?>
