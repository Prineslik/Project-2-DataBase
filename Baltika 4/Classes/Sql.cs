using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System;

namespace Baltika_4.Classes
{
    public static class Sql
    {

        #region DataSets

        public static DataSet[] dataSets = new DataSet[8]
        {
            new DataSet(),
            new DataSet(),
            new DataSet(),
            new DataSet(),
            new DataSet(),
            new DataSet(),
            new DataSet(),
            new DataSet()
        };

        #endregion

        #region Adapters

        public static OleDbDataAdapter[] adapters = new OleDbDataAdapter[8]
        {
            new OleDbDataAdapter(),
            new OleDbDataAdapter(),
            new OleDbDataAdapter(),
            new OleDbDataAdapter(),
            new OleDbDataAdapter(),
            new OleDbDataAdapter(),
            new OleDbDataAdapter(),
            new OleDbDataAdapter()
        };

        #endregion
        
        #region Dictionary

        public static Dictionary<int, string> selectQueries = new Dictionary<int, string>()
        {
            { 
                0, "SELECT Batches.[BatchID] as 'ИД партии', Batches.[CustomValue] as 'Таможенный номер', Batches.[DepartureDate] as 'Дата убытия', Batches.[ArriveDate] as 'Дата прибытия',"+
                " Batches.[OriginID] as 'ИД пункта отправления', Batches.[DestinationID] as 'ИД пункта назначения', Batches.[CustomClearance] as "+
                "'Необходимость таможенной декларации' FROM Batches"
            },
            {
                1, "SELECT Cargoes.[CargoID] as 'ИД груза', Cargoes.[SenderID] as 'ИД отправителя', Cargoes.[Shipment] as 'Название', Cargoes.[DeclareValue] as 'Заявленная величина груза', Cargoes.[Unit] as 'Единица измерения', Cargoes.[InsureValue] as 'Застрахованная величина груза', Cargoes.[INNSender] as 'ИНН отправителя',"+
                " Cargoes.[BankSender] as 'Банк отправителя', Cargoes.[AddressSender] as 'Адрес отправителя', Cargoes.[ConsigneeID] as 'ИД получателя', Cargoes.[INNConsignee] as 'ИНН получателя', Cargoes.[BankConsignee] as 'Банк получателя', Cargoes.[AddressConsignee] as 'Адрес получателя', Cargoes.[Comment] as 'Примечания', "+
                "Cargoes.[Number] as 'Номер в партии' FROM Cargoes"
            },
            {
                2, "SELECT Cargoes_Batches.[ID_CB] as 'ИД связи', Cargoes_Batches.[IDCargo] as 'ИД груза', Cargoes_Batches.[IDBatch] as 'ИД партии' FROM Cargoes_Batches"
            },
            {
                3, "SELECT ConsSender.[ID] as 'ИД', ConsSender.[NameCS] as 'Название' FROM ConsSender"
            },
            {
                4, "SELECT Ports.[ID] as 'ИД', Ports.[PortName] as 'Название' FROM Ports"
            },
            {
                5, "SELECT Ships.[ShipID] as 'ИД', Ships.[RegNumber] as 'Регистрационный номер', Ships.[Name] as 'Название', Ships.[TypeShipID] as 'ИД типа судна', Ships.[Skipper] as 'ФИО капитана', Ships.[Capacity] as 'Грузоподъемность', Ships.[Year] as 'Год постройки', Ships.[DockyerdID] as 'Порт приписки' FROM Ships"
            },
            {
                6, "SELECT Ships_Batches.[ID] as 'ИД связи', Ships_Batches.[ShipID] as 'ИД судна', Ships_Batches.[BatchID] as 'ИД груза' FROM Ships_Batches"
            },
            {
                7, "SELECT ShipsType.[TypeShipID] as 'ИД типа судна', ShipsType.[TypeShip] as 'Тип' FROM ShipsType"
            }
        };

        public static Dictionary<int, string> deleteQueries = new Dictionary<int, string>()
        {
            { 0, "delete from Batches where BatchID = "},
            { 1, "delete from Cargoes where CargoID = " },
            { 2, "delete from Cargoes_Batches where ID_CB = "},
            { 3, "delete from ConsSender where ID = "},
            { 4, "delete from Ports where ID = "},
            { 5, "delete from Ships where ShipID = "},
            { 6, "delete from Ships_Batches where ID = "},
            { 7, "delete from ShipsType where TypeShipID = "}
        };

        public static Dictionary<int, string> tables = new Dictionary<int, string>()
        {
            { 0, "[Batches]"},
            { 1, "[Cargoes]" },
            { 2, "[Cargoes_Batches]"},
            { 3, "[ConsSender]"},
            { 4, "[Ports]"},
            { 5, "[Ships]"},
            { 6, "[Ships_Batches]"},
            { 7, "[ShipsType]"}
        };

        #endregion

        private static readonly string connectionString = Properties.Settings.Default.connectionString;

        public static void ClearTable(DataGridView dg)
        {
            if (dg.DataSource != null)
            {
                dg.DataSource = null;
            }
            else
            {
                dg.Rows.Clear();
            }
        }

        public static void ConnectToAccess(DataSet ds, OleDbDataAdapter adapter, DataGridView dg, string query, string table)
        {
            ds.Clear();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    adapter = new OleDbDataAdapter(query, connectionString);
                    adapter.Fill(ds, table);
                    dg.DataSource = ds.Tables[0].DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }
    }
}
