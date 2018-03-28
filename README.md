
# Log4NetAppenders
## Synopsis

This C# solution provides 2 Log4Net appenders.
The first one is an appender to send mail through an Exchange Server.  
The second one is an appender to log exceptions (or whatever) as a [Work Item (Bug) in Visual Studio Online](https://www.visualstudio.com/en-us/get-started/work/create-your-backlog-vs).

## SendMailExchange
### Motivation
The configuration to do with the [SMTPAppender](https://logging.apache.org/log4net/release/sdk/log4net.Appender.SmtpAppender.html) provided by Log4Net is difficult when you have to send a mail through Exchange and a company proxy.  
Obviously this appender is not as powerful as the SmtpAppender, but he logs correctly what you want by email.
### Installation
You have to configure your Log4Net.config with your own settings, and put SendMailExchange.dll in the folder where Log4Net.dll is.  
Sample Log4Net.config :
```
<log4net>
  <!-- Beginning of the ExchangeAppender configuration-->
	<appender name="Exchange" type="SendMailExchange.ExchangeMailAppender, SendMailExchange">
		<recipient value="mail@whatever.com"> <!-- if not setted, will use the defaultRecipient setted in the configuration file-->
		<mailAddressForUrl value="mail@company.com"> <!-- if not setted, will use the defaultRecipient setted in the configuration file-->
		<layout type="log4net.Layout.PatternLayout,log4net">
			<conversionPattern value="LEVEL: %level %newlineDATE: %date{dd/MM/yyyy HH:mm:ss,fff}  %newlineLOGGER: %logger %newline%newline%message" />
		</layout>
	</appender>
	<!-- End of the ExchangeAppender configuration-->
	<root>
		<level value="ALL"/>
	</root>
	<logger name="DebugLogger">
		<level value="DEBUG"/>
	</logger>
	<logger name="MonitoringLogger">
		<level value="INFO"/>
	</logger>
	<logger name="ExceptionLogger">
		<level value="ERROR"/>
		<appender-ref ref="Exchange"/>
	</logger>
</log4net>
```
## StudioOnlineBugAppender
### Motivation
When you work with visual studio online, you may want to have backlog from your application bugs, in visual studio.
### Installation
You have to configure your Log4Net.config with your own settings, and put VisualStudioOnlineBugAppender.dll in the folder where Log4Net.dll is.  
Sample Log4Net.config :
```
<log4net>
  <!-- Beginning of the VisualStudioOnlineBugAppender configuration-->
	<appender name="OnlineBug" type="StudioOnlineBugAppender.OnlineBugAppender, StudioOnlineBugAppender">
		<vsoProject value ="MyProject"/>
		<vsoAccountName value ="MyAccountName"/>
		<proxyUrl value ="http://proxy.company:proxyport"/>
		<useProxy value ="true"/> <!-- don't set it or use false if you don't want to use a proxy-->
		<!-- If you want to authenticate in visual studio online with your token -->
		<vsoAuthenticateWithToken value ="true"/>
		<vsoToken value ="token"/>
		<!-- 
		If you want to authenticate with your visual studio online account :
		<vsoAuthenticateWithToken value ="false"/>
		<vsoUsername value="username"/>
		<vsoPassword value="password"/>
		-->
		<layout type="log4net.Layout.PatternLayout,log4net">
			<conversionPattern value="LEVEL: %level %newlineDATE: %date{dd/MM/yyyy HH:mm:ss,fff}  %newlineLOGGER: %logger %newline%newline%message" />
		</layout>
	</appender>
	<!-- End of the VisualStudioOnlineBugAppender configuration-->
	<root>
		<level value="ALL"/>
	</root>
	<logger name="DebugLogger">
		<level value="DEBUG"/>
	</logger>
	<logger name="MonitoringLogger">
		<level value="INFO"/>
	</logger>
	<logger name="ExceptionLogger">
		<level value="ERROR"/>
		<appender-ref ref="OnlineBug"/>
	</logger>
</log4net>
```
