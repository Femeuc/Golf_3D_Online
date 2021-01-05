using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Com.Femeuc.Golf3DOnline
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        string gameVersion = "1";
        [SerializeField]
        private byte maxPlayersPerRoom = 4;
        [SerializeField]
        private GameObject controlPanel;
        [SerializeField]
        private GameObject progressLabel;
        bool isConnecting;

        void Awake()
        {          
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        public override void OnConnectedToMaster()
        {
            if (isConnecting)
            {
                Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
                PhotonNetwork.JoinRandomRoom();
                isConnecting = false;
            }
        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            isConnecting = false;
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
            PhotonNetwork.LoadLevel("Room");
        }

        public void Connect()
        {

            progressLabel.SetActive(true);
            controlPanel.SetActive(false);
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                isConnecting = PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
        }
    }
}