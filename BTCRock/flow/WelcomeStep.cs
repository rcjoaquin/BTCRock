using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTCRock.flow
{
    public class WelcomeStep
    {
        private PrintStream printer;
        private InputStream input;
        private String mainBTCAddress;
        private float fee;
        private String endPoint;
        private int port;
        private String user;
        private String password;

        public WelcomeStep(PrintStream printer, InputStream input,
                String mainBTCAddress, float fee, String endPoint, int port,
                String user, String password) {
            this.printer = printer;
            this.input = input;
            this.mainBTCAddress = mainBTCAddress;
            this.fee = fee;
            this.endPoint = endPoint;
            this.port = port;
            this.user = user;
            this.password = password;
        }

        public Step run() {
            this.printer.print(buildWelcomeMessage());
            this.waitForKey();
            return new ReadChoice(this.printer, this.input, this.mainBTCAddress,
                    this.fee, this.endPoint, this.port, this.user, this.password);
        }

        private void waitForKey() 
        {
            this.printer.println("\n PRESS ENTER KEY TO CONTINUE...");
            InputStreamReader converter = new InputStreamReader(this.input);
            BufferedReader in = new BufferedReader(converter);
            try {
                in.readLine();
            } catch (IOException e) {
                throw new RuntimeException(e.getMessage());
            }
        }

        private static String buildWelcomeMessage() {
            StringBuffer header = new StringBuffer();
            try {
                InputStream stream = WelcomeStep.class
                        .getResourceAsStream("/welcome.txt");
                BufferedReader reader = new BufferedReader(new InputStreamReader(
                        stream));
                String line;
                while ((line = reader.readLine()) != null) {
                    header.append(line).append("\n");
                }
                stream.close();
                reader.close();
            } catch (IOException e) {
                throw new RuntimeException(String.format(
                        "Exception when loading welcome message. %s",
                        e.getMessage()));
            }
            return header.toString();
        }
    }
}
