<!DOCTYPE html>
<html>
<head>
    <title>FormValidation demo</title>
    <link rel="stylesheet" href="vendor/bootstrap/css/bootstrap.css"/>
</head>
<body>
    <div class="container">
        <div class="row">
            <h2>Form data</h2>
            <hr/>
            <p>This is a simple page showing the data you have just submitted</p>
            <pre><?php print_r($_POST); ?></pre>
            <p><a href="javascript:history.go(-1);">back to demo</a></p>
        </div>
    </div>
</body>
</html>