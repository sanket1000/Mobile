-- =============================================
-- Script Template
-- =============================================
--- Service endpoints

----- SERVER S1-------------------------------
--Messaging(MSMQ) Svc: net.tcp://10.1.10.22:9002/CoreAPIMsgHandlerSvc
--Install - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe "E:\CoreAPI\MSMQ\Host\Core.API.MessagingSvcHost.exe"
--Uninstall - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe /u "E:\CoreAPI\MSMQ\Host\Core.API.MessagingSvcHost.exe"

-- SERVER - S3-------------
--Install - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe "C:\sanket\CoreAPIHH2\MSMQSvc\Host\Core.API.MessagingSvcHost.exe"
--Uninstall - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe /u "C:\sanket\CoreAPIHH2\MSMQSvc\Host\Core.API.MessagingSvcHost.exe"

-- SERVER - S3-------------
--Install - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe "C:\sanket\CoreAPIHH2\MSMQSvc\Host\Core.API.MessagingSvcHost.exe"
--Uninstall - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe /u "C:\sanket\CoreAPIHH2\MSMQSvc\Host\Core.API.MessagingSvcHost.exe"


-- SERVER - S9---zenq
--Install - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe "E:\Development Data\CoreAPI\MSMQ\Host\Core.API.MessagingSvcHost.exe"
--Uninstall - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe /u "E:\Development Data\CoreAPI\MSMQ\Host\Core.API.MessagingSvcHost.exe"


--User Svc: net.tcp://10.1.10.22:9004/UserSvc
----- SERVER S1-------------------------------
--Install - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe "E:\CoreAPI\UserSvc\Host\Core.API.UserSvcHost.exe"
--Uninstall - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe /u "E:\CoreAPI\UserSvc\Core.API.UserSvcHost.exe"

-- SERVER - S3-------------
--Install - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe "C:\sanket\CoreAPIHH2\UserSvc\Host\Core.API.UserSvcHost.exe"
--Uninstall - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe /u "C:\sanket\CoreAPIHH2\UserSvc\Host\Core.API.UserSvcHost.exe"

-- Server - S9 - Zenq
--Install - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe "E:\Development Data\CoreAPI\UserSvc\Host\Core.API.UserSvcHost.exe"
--Uninstall - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe /u "E:\Development Data\CoreAPI\UserSvc\Host\Core.API.UserSvcHost.exe"


--Config Svc: net.tcp://10.1.10.22:9006/CoreAPIConfigSvc
----- SERVER S1-------------------------------
--Install - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe "E:\CoreAPI\ConfigSvc\Core.API.ConfigSvcHost.exe"
--Uninstall - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe /u "E:\CoreAPI\ConfigSvc\Core.API.ConfigSvcHost.exe"

-- SERVER - S3-------------
--Install - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe "C:\sanket\CoreAPIHH2\ConfigSvc\Host\Core.API.ConfigSvcHost.exe"
--Uninstall - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe /u "C:\sanket\CoreAPIHH2\ConfigSvc\Host\Core.API.ConfigSvcHost.exe"

-- SERVER - S9-- zenq
--Install - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe "E:\Development Data\CoreAPI\ConfigSvc\Host\Core.API.ConfigSvcHost.exe"
--Uninstall - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe /u "E:\Development Data\CoreAPI\UserSvc\Host\Core.API.ConfigSvcHost.exe"

--DocumentSvc: net.tcp://10.1.10.22:9008/DocumentSvc
-- SERVER - S3-------------
--Install - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe "C:\sanket\CoreAPIHH2\DocumentSvc\Host\Core.API.DocumentSvcHost.exe"
--Uninstall - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe /u "C:\sanket\CoreAPIHH2\DocumentSvc\Host\Core.API.DocumentSvcHost.exe"

----- SERVER S1-------------------------------
--Install - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe "E:\CoreAPI\DocumentSvc\Host\Core.API.DocumentSvcHost.exe"
--Uninstall - C:\Windows\Microsoft.NET\Framework\v4.0.30319\Installutil.exe /u "E:\CoreAPI\DocumentSvc\Host\Core.API.DocumentSvcHost.exe"

----------------------------------------

-- SaveInvoiceNotes (saving commetns)
-- no need to pass Priority as it is hardcoded to Normal 
-- Also need to discuss how to remember note per session. In timberscan it remembers note starttime and user can update same note provided he is on the same invoice.