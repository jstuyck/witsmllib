

namespace witsmllib
{
   
    public interface WitsmlAccessListener
    {

        /**
         * This method is called whenever the remote WITSML server has
         * been accessed.
         *
         * @param event  Event describing the access. Never null.
         */

        //TODO
        // public void accessPerformed(WitsmlAccessEvent @event);
        void requestSent(WitsmlRequest request);

        /// <summary>
        /// Method called whenever a response is delivered from the remote WITSML server. 
        /// </summary>
        /// <param name="response">response - The response delivered. Never null.</param>
        void responseReceived(WitsmlResponse response);
        

    }
}