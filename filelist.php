<?php
$directory = "files";
$items = new RecursiveIteratorIterator(new RecursiveDirectoryIterator($directory));
foreach($items as $item) echo str_replace("$directory/", '', $item), PHP_EOL;
?>