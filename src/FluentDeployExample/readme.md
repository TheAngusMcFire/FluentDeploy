

#### The ssh library only supports the old openssh key format, use this line to convert
    ssh-keygen -p -f file -m pem -P passphrase -N passphrase

#### For sftp to work properly on the remote host replace:
    Subsystem sftp /usr/lib/openssh/sftp-server
with:    

    Subsystem sftp sudo -n true && sudo -n /usr/lib/openssh/sftp-server || /usr/lib/openssh/sftp-server

This will run sftp as sudo if the user has sudo privileges


#### Code example I used to read / write with streams from the ssh client, we will need this when we want to limit the amount of text exchanged

```c#
 public void Test()
{           
            //Renci.SshNet.

            //var stream = client.CreateShellStream("term1", 100, 100, 100, 100, 100);
            
            /*
            var inStream = new PipeStream();
            var outStream = new PipeStream();
            outStream.BlockLastReadBuffer = false;

            //var reader = new StreamReader(outStream);
            var writer = new StreamWriter(inStream);

            var shell = client.CreateShell(inStream, outStream, Stream.Null);
            shell.Start();
            
            
            writer.WriteLine("ls /bin\n");
            writer.Flush();
            Task.Delay(100).Wait();
            var buffer = new byte[500];
            //var vals = outStream.Read(buffer, 0, buffer.Length);
            outStream.BlockLastReadBuffer = false;
            var reader = new StreamReader(outStream, Encoding.UTF8, true, 1, false);
            //writer.Flush();
            while (true)
            {
                Console.WriteLine(reader.ReadLine());
            }
                */

            //for (var i = 0; i < 100; i++)
            //{
            //    var cmd = client.CreateCommand("read");
            //    var txt = cmd.BeginExecute();
            //    txt.AsyncWaitHandle.WaitOne();
            //    var code = cmd.ExitStatus;
            //    Console.WriteLine(txt);    
            //}
}```